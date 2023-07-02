using System.ComponentModel.DataAnnotations;

namespace EmploymentExchange.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(30)]
        public string Name { get; set; }
        [Required]
        public bool State { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
