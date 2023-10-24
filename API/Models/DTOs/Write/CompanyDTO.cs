using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CompanyDTO
    {
        [Required, MaxLength(50, ErrorMessage = "Name must have maximum 50 characters")]
        public string Name { get; set; } = null!;
        [Required]
        public string Location { get; set; } = null!;
        [Required, EmailAddress, MaxLength(50, ErrorMessage = "Email must have maximum 50 characters")]
        public string RecruiterEmail { get; set; } = null!;
        public string? Logo { get; set; }
        public string? URL { get; set; }
    }
}
