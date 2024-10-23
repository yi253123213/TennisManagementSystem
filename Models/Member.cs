using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TennisFinalGrp339.Models;

public partial class Member
{
    [Key]
    public int MemberId { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public bool Active { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


}
