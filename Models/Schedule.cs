using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TennisFinalGrp339.Models;

public partial class Schedule
{
    [Key]
    public int ScheduleId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public string? Description { get; set; }
    public DateTime ScheduledDate { get; set; } // To enforce before-date enrollments

    // Foreign Key to Coach
    public int CoachId { get; set; }
    public Coach Coach { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}
