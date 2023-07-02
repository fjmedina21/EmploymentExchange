using System.ComponentModel.DataAnnotations;

namespace EmploymentExchange.Models
{
    public class LoginDTO
    {
        [Required, EmailAddress, MaxLength(50, ErrorMessage = "Email must have maximum 50 characters")]
        public string Email { get; set; }
        [Required, MinLength(8, ErrorMessage = "Password must be minimum 8 charaters"), MaxLength(100)]
        public string Password { get; set; }
    }
}
