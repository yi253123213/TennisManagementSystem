using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using TennisFinalGrp339.Data;
using TennisFinalGrp339.Models;

namespace TennisFinalGrp339.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string ReturnUrl { get; set; } = string.Empty;

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Required]
            [Display(Name = "User Role")]
            public string UserRole { get; set; } = string.Empty;

            [Display(Name = "Biography")]
            public string? Biography { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Biography = Input.Biography,
                    IsCoach = Input.UserRole == "Coach",
                    EmailConfirmed = false // Default to false; will confirm via link
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // Ensure the role exists
                    if (!await _roleManager.RoleExistsAsync(Input.UserRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Input.UserRole));
                    }

                    // Assign role to user
                    await _userManager.AddToRoleAsync(user, Input.UserRole);

                    // Create Coach or Member object and set ID in ApplicationUser
                    if (Input.UserRole == "Coach")
                    {
                        var coach = new Coach
                        {
                            FirstName = Input.FirstName,
                            LastName = Input.LastName,
                            Biography = Input.Biography,

                        };
                        _context.Coach.Add(coach);
                        await _context.SaveChangesAsync();

                        // Assign CoachId to user
                        user.CoachId = coach.CoachId;
                        _context.Users.Update(user); // Save CoachId or MemberId to ApplicationUser
                        await _context.SaveChangesAsync();
                    }
                    else if (Input.UserRole == "Member")
                    {
                        var member = new Member
                        {
                            FirstName = Input.FirstName,
                            LastName = Input.LastName,
                            Email = Input.Email,
                            Active = true,

                        };
                        _context.Member.Add(member);
                        await _context.SaveChangesAsync();

                        // Assign MemberId to user
                        user.MemberId = member.MemberId;
                        _context.Users.Update(user); // Save CoachId or MemberId to ApplicationUser
                        await _context.SaveChangesAsync();
                    }



                    _logger.LogInformation("User created a new account with password.");

                    // Generate email confirmation token and link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    // Send email (optional)
                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // Redirect to confirmation page with the link
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, userRole = Input.UserRole, link = callbackUrl });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Redisplay the form if validation fails
            return Page();
        }

    }
}
