namespace EmploymentExchangeAPI.Models
{
    public class GetCompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string? Logo { get; set; }
        public string? URL { get; set; }
    }
}
