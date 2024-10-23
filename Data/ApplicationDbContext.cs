using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisFinalGrp339.Models;

namespace TennisFinalGrp339.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TennisFinalGrp339.Models.Coach> Coach { get; set; } = default!;
        public DbSet<TennisFinalGrp339.Models.Member> Member { get; set; } = default!;
        public DbSet<TennisFinalGrp339.Models.Schedule> Schedule { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coach>(entity =>
            {
                entity.Property(e => e.CoachId).ValueGeneratedNever();
                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsFixedLength();
                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).ValueGeneratedNever();
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
                entity.Property(e => e.ScheduleId).ValueGeneratedNever();
                entity.Property(e => e.Location)
                    .HasMaxLength(200)
                    .IsFixedLength();
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}