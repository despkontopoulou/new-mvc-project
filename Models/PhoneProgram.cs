using System;
using System.Collections.Generic;

namespace NewMVCProject.Models;

public partial class PhoneProgram
{
    public string ProgramName { get; set; } = null!;

    public string? Benefits { get; set; }

    public decimal? Charge { get; set; }
}
