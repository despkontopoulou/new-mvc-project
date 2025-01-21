using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewMVCProject.Models;

public partial class Call
{
    [Key]
    public int CallId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Required]
    [ForeignKey("Phone")]
    [StringLength(15)]
    [Column(TypeName = "varchar(15)")]
    [DisplayName("Phone Number")]
    public string? PhoneNumber { get; set; }

    [DisplayName("Related Phone")]
    public virtual Phone? PhoneNumberNavigation { get; set; }
}
