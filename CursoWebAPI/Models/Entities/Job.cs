using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentExchangeAPI.Models
{
    public class Job
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, ForeignKey(nameof(CompanyId))]
        public Guid CompanyId { get; set; }
        [Required, ForeignKey(nameof(JobTypeId))]
        public Guid JobTypeId { get; set; }
        [Required, ForeignKey(nameof(JobPositionId))]
        public Guid JobPositionId { get; set; }
        [Required, Column(TypeName = "decimal(9,2)")]
        public decimal Salary { get; set; }
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        [Required]
        public bool State { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public JobPosition JobPosition { get; set; } = null!;
        public JobType JobType { get; set; } = null!;
        public Company Company { get; set; } = null!;

        public IList<User> Users { get; set; } = new List<User>();
    }
}
