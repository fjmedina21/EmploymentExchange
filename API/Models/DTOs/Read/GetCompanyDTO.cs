namespace API.Models
{
    public class GetCompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? Logo { get; set; } 
        public string? URL { get; set; } 
    }
}
