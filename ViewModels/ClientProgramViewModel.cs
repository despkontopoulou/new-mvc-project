using System.ComponentModel.DataAnnotations;

namespace NewMVCProject.ViewModels
{
    public class ClientProgramViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string ProgramName { get; set; }

    }
}
