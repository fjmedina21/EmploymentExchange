namespace EmploymentExchangeAPI.Models.ManyToMany
{
    public class JobUser
    {
        public Guid UserId { get; set; }
        public Guid JobId { get; set; }

        public User Users { get; set; }
        public Job Jobs { get; set; }
    }
}
