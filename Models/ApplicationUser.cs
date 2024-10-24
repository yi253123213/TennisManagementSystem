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
    }
}
