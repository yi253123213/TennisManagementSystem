using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TennisFinalGrp339.Models;

public partial class Schedule
{
    [Key]
    public int ScheduleId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string? Location { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; } // To enforce before-date enrollments

    [Required]
    // Foreign Key to Coach
    public int CoachId { get; set; }
    public Coach? Coach { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}
