using System.ComponentModel.DataAnnotations;

namespace EmploymentExchangeAPI.Models
{
    public class JobPositionDTO
    {
        [Required, MaxLength(30, ErrorMessage = "Name must have maximum 30 characters")]
        public string Name { get; set; } = null!;
        [Required]
        public Guid CategoryId { get; set; }
    }
}
