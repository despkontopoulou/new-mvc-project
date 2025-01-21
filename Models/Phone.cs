using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewMVCProject.Models;

public partial class Phone
{
    [Key]  
    [StringLength(15)]
    [Column(TypeName = "varchar(15)")]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string? ProgramName { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
