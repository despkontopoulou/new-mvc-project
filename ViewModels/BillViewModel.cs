using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NewMVCProject.ViewModels
{
    public class BillViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        public decimal Costs { get; set; }
    }
}
