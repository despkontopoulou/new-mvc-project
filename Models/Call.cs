using System;
using System.Collections.Generic;

namespace NewMVCProject.Models;

public partial class Call
{
    public int CallId { get; set; }

    public string? Description { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual Phone? PhoneNumberNavigation { get; set; }
}
