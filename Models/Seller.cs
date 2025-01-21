using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewMVCProject.Models;

public partial class Seller
{
    [Key]
    public int SellerId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
