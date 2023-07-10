namespace EmploymentExchangeAPI.Models
{
    public class GetJobDTO
    {
        public Guid Id { get; set; }
        public string Company { get; set; }
        public string RecruiterEmail { get; set; }
        public string? Logo { get; set; }
        public string? URL { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}
