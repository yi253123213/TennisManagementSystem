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

        // CoachId and MemberId to link to specific role entities
        public int? MemberId { get; set; }

        public Member? Member { get; set; } // Navigation property

        // Add CoachId to link ApplicationUser with Coach
        public int? CoachId { get; set; }

        public Coach? Coach { get; set; } // Navigation property
    }

}
