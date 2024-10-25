using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TennisFinalGrp339.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public string? FirstName { get; set; }

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public bool Active { get; set; }

        public ApplicationUser ApplicationUser { get; set; } // Navigation property

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}
