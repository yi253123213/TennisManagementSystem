using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TennisFinalGrp339.Data;
using TennisFinalGrp339.Models;

namespace TennisFinalGrp339.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchedulesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.Schedule
                .Include(s => s.Coach) // Include Coach entity
                .Include(s => s.Enrollments) // Include enrollments
                .ToListAsync();

            return View(await _context.Schedule.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Coach) // Include Coach entity
                .Include(s => s.Enrollments) // Include enrollments for the schedule
                    .ThenInclude(e => e.Member) // Include each member in the enrollments
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewBag.CoachId = new SelectList(_context.Coach, "CoachId", "FirstName"); // Assuming 'FirstName' for simplicity, you can concatenate FirstName and LastName if needed.
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,Name,Location,Description,ScheduledDate,CoachId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload the dropdown list in case of errors
            ViewBag.CoachId = new SelectList(_context.Coach, "CoachId", "FirstName", schedule.CoachId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewBag.CoachId = new SelectList(_context.Coach, "CoachId", "FirstName"); // Assuming 'FirstName' for simplicity, you can concatenate FirstName and LastName if needed.
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,Name,Location,Description,ScheduledDate,CoachId")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // Reload the dropdown list in case of errors
            ViewBag.CoachId = new SelectList(_context.Coach, "CoachId", "FirstName", schedule.CoachId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Enroll(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login"); // Redirect to login if not authenticated
            }

            if (user.MemberId == null)
            {
                return BadRequest("MemberId is missing for the current user.");
            }
            var memberId = user.MemberId.Value;

            // Get the schedule by ID
            var schedule = _context.Schedule.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            if (schedule.ScheduledDate <= DateTime.Now)
            {
                // Display a message or redirect to an appropriate page if enrollment is not allowed
                TempData["ErrorMessage"] = "Enrollment is not allowed on or after the scheduled date.";
                return RedirectToAction(nameof(Index));
            }

            var enrollment = new Enrollment
            {
                ScheduleId = id,
                MemberId = memberId,
                EnrolledOn = DateTime.Now
            };

            _context.Enrollment.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Unenroll(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.MemberId == null)
            {
                return BadRequest("MemberId is missing for the current user.");
            }
            var memberId = user.MemberId.Value;


            var enrollment = await _context.Enrollment
                .FirstOrDefaultAsync(e => e.ScheduleId == id && e.MemberId == memberId);

            if (enrollment != null)
            {
                _context.Enrollment.Remove(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CoachSchedules()
        {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.CoachId == null)
            {
                return RedirectToAction("Index"); // Redirect if not a coach
            }

            // Get schedules for the logged-in coach
            var schedules = await _context.Schedule
                .Where(s => s.CoachId == user.CoachId)
                .ToListAsync();

            return View("CoachSchedules", schedules); // Use a new view
        }

        public async Task<IActionResult> MemberSchedules()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.MemberId == null)
            {
                return RedirectToAction("Index"); // Redirect if not a member
            }

            // Retrieve schedules with enrollments data for the logged-in member
            var schedules = await _context.Schedule
                .Where(s => s.Enrollments.Any(e => e.MemberId == user.MemberId))
                .Include(s => s.Enrollments) // Include Enrollments data
                .Include(s => s.Coach)        // Optionally include Coach if needed
                .ToListAsync();

            return View("MemberSchedules", schedules);
        }



        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.ScheduleId == id);
        }
    }
}
