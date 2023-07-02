namespace EmploymentExchange.Models
{
    public class LoggedInDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
        public string token { get; set; }
    }
}
