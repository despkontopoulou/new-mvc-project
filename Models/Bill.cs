using System;
using System.Collections.Generic;

namespace NewMVCProject.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public string? PhoneNumber { get; set; }

    public decimal? Costs { get; set; }

    public virtual Phone? PhoneNumberNavigation { get; set; }
}
