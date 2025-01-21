using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewMVCProject.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }


    [StringLength(50)]
    [Unicode(false)]
    [Display(Name = "Username")]
    public string? Username { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    [Display(Name = "User Role")]
    public string? Property { get; set; }

    [Required]
    [Display(Name = "Password")]
    public string PasswordHash { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [InverseProperty("User")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    [InverseProperty("User")]
    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
