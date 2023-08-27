namespace EmploymentExchangeAPI.Models
{
    public class GetJobDTO
    {
        public Guid Id { get; set; }
        public string Company { get; set; } = null!;
        public string RecruiterEmail { get; set; } = null!;
        public string? Logo { get; set; }
        public string? URL { get; set; }
        public string Type { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal Salary { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}
