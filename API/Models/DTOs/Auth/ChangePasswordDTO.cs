using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ChangePasswordDTO
    {
        [Required,
         RegularExpression(
            "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(~`!@#$%^&*()-_+={}[]|\\;:\"<>,./?).{10,}$",
            ErrorMessage = "Passwords must contain: a minimum of 1 lower case letter [a-z], a minimum of 1 upper case letter [A-Z], a minimum of 1 numeric character [0-9], a minimum of 1 special character: ~`!@#$%^&*()-_+={}[]|\\;:\"<>,./? and must be at least 10 characters."),
        ]
        public string NewPassword { get; set; } = null!;
        [Required]
        public string CurrentPassword { get; set; } = null!;
    }
}
