using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TennisFinalGrp339.Models;

public partial class Schedule
{
    [Key]
    public int ScheduleId { get; set; }

    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]

    public string? Name { get; set; } = null!;

    [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
    public string? Location { get; set; }

    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Scheduled Date")]
   
    public DateTime ScheduledDate { get; set; } // To enforce before-date enrollments

    // Foreign Key to Coach
    public int CoachId { get; set; }
    public Coach? Coach { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}
