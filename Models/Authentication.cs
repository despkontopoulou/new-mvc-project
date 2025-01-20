using System.ComponentModel.DataAnnotations;

namespace NewMVCProject.Models
{
    public class Authentication
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; }
    }
}