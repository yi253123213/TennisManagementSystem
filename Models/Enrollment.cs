using System;
using System.Collections.Generic;

namespace TennisFinalGrp339.Models;

public class Enrollment
{
    // Primary Key
    public int EnrollmentId { get; set; }

    // Foreign Key for Member
    public int MemberId { get; set; }

    // Foreign Key for Schedule
    public int ScheduleId { get; set; }

    // Additional properties
    public DateTime EnrolledOn { get; set; }

    // Navigation Properties
    public Member Member { get; set; } = default!;
    public Schedule Schedule { get; set; } = default!;
}
