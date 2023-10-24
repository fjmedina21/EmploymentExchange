using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CategoryDTO
    {
        [Required, MaxLength(30, ErrorMessage = "Name must have maximum 30 characters")]
        public string Name { get; set; } = null!;
    }
}
