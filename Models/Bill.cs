using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace NewMVCProject.Models;

public partial class Bill
{
    [Key]
    public int BillId { get; set; }

    [Required]
    [ForeignKey("Phone")]
    [StringLength(15)]
    [Column(TypeName = "varchar(15)")]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "decimal(7,2)")]
    public decimal? Costs { get; set; }

    [DisplayName("Billed Phone Number")]
    public virtual Phone? PhoneNumberNavigation { get; set; }
}
