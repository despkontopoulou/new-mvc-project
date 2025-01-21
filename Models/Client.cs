using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewMVCProject.Models;

public partial class Client
{
    [Key]
    public int ClientId { get; set; }

    [Required]
    [StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    [Display(Name = "AFM")]
    public string? Afm { get; set; }

    [Required]
    [StringLength(15)]
    [Column(TypeName = "varchar(15)")]
    [Display(Name = "Phone Number")]
    [ForeignKey("Phone")]
    public string? PhoneNumber { get; set; }

    [Required]
    [ForeignKey("User")]
    [Display(Name = "User ID")]
    public int? UserId { get; set; }

    [Display(Name = "Phone Details")]
    public virtual Phone? PhoneNumberNavigation { get; set; }

    public virtual User? User { get; set; }
}
