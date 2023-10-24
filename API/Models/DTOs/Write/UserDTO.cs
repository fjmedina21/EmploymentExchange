using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserDTO
    {
        [Required, MaxLength(30, ErrorMessage = "First Name must have maximum 30 characters")]
        public string FirstName { get; set; } = null!;
        [Required, MaxLength(30, ErrorMessage = "Last Name must have maximum 30 characters")]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress, MaxLength(50, ErrorMessage = "Email must have maximum 50 characters")]
        public string Email { get; set; } = null!;
        [Required,
         RegularExpression(
            "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(~`!@#$%^&*()-_+={}[]|\\;:\"<>,./?).{10,}$",
            ErrorMessage = "Passwords must contain: a minimum of 1 lower case letter [a-z], a minimum of 1 upper case letter [A-Z], a minimum of 1 numeric character [0-9], a minimum of 1 special character: ~`!@#$%^&*()-_+={}[]|\\;:\"<>,./? and must be at least 10 characters."),   
        ]
        public string Password { get; set; } = null!;
    }
}
