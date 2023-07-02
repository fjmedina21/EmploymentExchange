using System.ComponentModel.DataAnnotations;

namespace EmploymentExchange.Models
{
    public class JobDTO
    {
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid JobTypeId { get; set; }
        [Required]
        public Guid JobPositionId { get; set; }
        [Required]
        public decimal Salary { get; set; }
        public string? Description { get; set; }
    }
}
