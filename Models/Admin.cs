using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewMVCProject.Models;

public partial class Admin
{
    [Key]
    public int AdminId { get; set; }

    [Required]
    [ForeignKey("User")]
    public int? UserId { get; set; }

    [InverseProperty("Admins")]
    public virtual User? User { get; set; }
}
