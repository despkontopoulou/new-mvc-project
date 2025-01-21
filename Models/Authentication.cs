using System.ComponentModel.DataAnnotations;

namespace NewMVCProject.Models
{
    public class Authentication
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(4)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; }
    }
}