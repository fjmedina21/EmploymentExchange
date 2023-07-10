using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentExchangeAPI.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, EmailAddress, StringLength(50)]
        public string RecruiterEmail { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Location { get; set; }
        public string? Logo { get; set; }
        public string? URL { get; set; }
        [Required]
        public bool State { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
