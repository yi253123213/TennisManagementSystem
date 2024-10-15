﻿using System;
using System.Collections.Generic;

namespace DbFirstEF.Models;

public partial class Coach
{
    public int CoachId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }

    public byte[]? Photo { get; set; }


}
