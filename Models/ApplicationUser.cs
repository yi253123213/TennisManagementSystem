using Microsoft.AspNetCore.Identity;

namespace TennisFinalGrp339.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        public string? Biography { get; set; }

        [PersonalData]
        public bool IsCoach { get; set; }

        // Add MemberId to link ApplicationUser with Member
        public int? MemberId { get; set; }

        // Add CoachId to link ApplicationUser with Coach
        public int? CoachId { get; set; }

        public Member? Member { get; set; } // Navigation property
        public Coach? Coach { get; set; }   // Navigation property

    }
}
