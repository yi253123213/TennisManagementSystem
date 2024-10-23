using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TennisFinalGrp339.Models;

public partial class Coach
{
    [Key]
    public int CoachId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }

    public byte[]? Photo { get; set; }
}
