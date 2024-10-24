using System.ComponentModel.DataAnnotations;

namespace TennisFinalGrp339.Models
{
    public class Coach
    {
        [Key]
        public int CoachId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? Biography { get; set; }

        public byte[]? Photo { get; set; }
    }
}
