using System;
using System.Collections.Generic;

namespace NewMVCProject.Models;

public partial class Phone
{
    public string PhoneNumber { get; set; } = null!;

    public string? ProgramName { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
