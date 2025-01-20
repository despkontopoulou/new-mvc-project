using System;
using System.Collections.Generic;

namespace NewMVCProject.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? Afm { get; set; }

    public string? PhoneNumber { get; set; }

    public int? UserId { get; set; }

    public virtual Phone? PhoneNumberNavigation { get; set; }

    public virtual User? User { get; set; }
}
