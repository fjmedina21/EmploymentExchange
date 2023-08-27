using System.ComponentModel.DataAnnotations;

namespace EmploymentExchangeAPI.Models
{
    public class ChangePasswordDTO
    {
        [Required, StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "Password must be minimum 8 characters")]
        public string NewPassword { get; set; } = null!;
        [Required]
        public string CurrentPassword { get; set; } = null!;
    }
}
