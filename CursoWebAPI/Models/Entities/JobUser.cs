namespace EmploymentExchange.Models
{
    public class JobUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public User User { get; set; }
        public Job Job { get; set; }
    }
}
