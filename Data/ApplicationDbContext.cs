using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisFinalGrp339.Models;

namespace TennisFinalGrp339.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Coach> Coach { get; set; } = default!;
        public DbSet<Member> Member { get; set; } = default!;
        public DbSet<Schedule> Schedule { get; set; } = default!;
        public DbSet<Enrollment> Enrollment { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coach>(entity =>
            {
                entity.HasKey(e => e.CoachId);
                entity.Property(e => e.CoachId).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsFixedLength();
                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsFixedLength();
                entity.Property(e => e.Biography)
                    .HasMaxLength(1000); // Adjust as needed
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MemberId);
                entity.Property(e => e.MemberId).ValueGeneratedOnAdd();
                entity.Property(e => e.Email)
                    .HasMaxLength(400)
                    .IsFixedLength();
                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsFixedLength();
                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);
                entity.Property(e => e.ScheduleId).ValueGeneratedOnAdd();
                entity.Property(e => e.Location)
                    .HasMaxLength(200)
                    .IsFixedLength();
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsFixedLength();

                // One Coach to Many Schedules
                entity.HasOne(s => s.Coach)
                      .WithMany()
                      .HasForeignKey(s => s.CoachId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Enrollment relationship
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId);
                entity.HasOne(e => e.Member)
                    .WithMany(m => m.Enrollments)
                    .HasForeignKey(e => e.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Schedule)
                    .WithMany(s => s.Enrollments)
                    .HasForeignKey(e => e.ScheduleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Member)
                .WithOne()
                .HasForeignKey<ApplicationUser>(a => a.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete Member when ApplicationUser is deleted

            
            modelBuilder.Entity<Member>()
                .HasOne<ApplicationUser>()
                .WithOne(a => a.Member)
                .HasForeignKey<ApplicationUser>(a => a.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // Delete ApplicationUser when Member is deleted

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
