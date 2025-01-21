using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewMVCProject.Models;

public partial class PhoneProgram
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string ProgramName { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Benefits { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Charge { get; set; }
}
