using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NewMVCProject.ViewModels
{
    public class PhoneProgramViewModel
    {
        [Key]
        [StringLength(50, ErrorMessage = "Program name cannot exceed 50 characters.")]
        [Required(ErrorMessage = "Program name is required.")]
        [Display(Name="Program Name")]
        public string ProgramName { get; set; } = null!;

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Benefits are required.")]
        public string Benefits { get; set; } = null!;

        [Column(TypeName = "decimal(5, 2)")]
        [Range(0, 999.99, ErrorMessage = "Charge must be between 0 and 999.99.")]
        [Required(ErrorMessage = "Charge is required.")]
        [DataType(DataType.Currency)]
        public decimal? Charge { get; set; }
    }
}
