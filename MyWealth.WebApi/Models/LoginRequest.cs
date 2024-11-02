using System.ComponentModel.DataAnnotations;

namespace MyWealth.WebApi.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password cannot be under 8 over characters")]
        [MaxLength(20, ErrorMessage = "Password cannot be over 20 over characters")]
        public string Password { get; set; }
    }
}
