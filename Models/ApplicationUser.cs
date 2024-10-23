using Microsoft.AspNetCore.Identity;

namespace TennisFinalGrp339.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }

        public string? Biography { get; set; } // To store biography
        [PersonalData]
        public bool IsCoach { get; set; } // To differentiate between Member and Coach

    }
}
