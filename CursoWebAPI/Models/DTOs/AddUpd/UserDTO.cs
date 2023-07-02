using System.ComponentModel.DataAnnotations;

namespace EmploymentExchange.Models
{
    public class UserDTO
    {
        [Required, MaxLength(30, ErrorMessage = "First Name must have maximum 30 characters")]
        public string FirstName { get; set; }
        [Required, MaxLength(30, ErrorMessage = "Last Name must have maximum 30 characters")]
        public string LastName { get; set; }
        [Required, EmailAddress, MaxLength(50, ErrorMessage = "Email must have maximum 50 characters")]
        public string Email { get; set; }
        [Required, MinLength(8, ErrorMessage = "Password must be minimum 8 charaters"), MaxLength(100)]
        public string Password { get; set; }
        public string? Photo { get; set; }
    }
}
