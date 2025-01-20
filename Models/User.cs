using System;
using System.Collections.Generic;

namespace NewMVCProject.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? Property { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
