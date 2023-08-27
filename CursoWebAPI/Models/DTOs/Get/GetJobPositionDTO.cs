namespace EmploymentExchangeAPI.Models
{
    public class GetJobPositionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
    }
}
