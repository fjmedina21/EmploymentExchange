using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Data
{
    public class MyDBContext : DbContext
    {

        public MyDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<JobType> JobTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<JobType> defaultJobTypes = new List<JobType>();
            defaultJobTypes.Add(new JobType { Name = "Full-time" });
            defaultJobTypes.Add(new JobType { Name = "Part-time" });
            defaultJobTypes.Add(new JobType { Name = "Contract" });
            defaultJobTypes.Add(new JobType { Name = "Internship" });

            List<Role> defaultRolesValues = new List<Role>();
            defaultRolesValues.Add(new Role { Name = "Admin", Description = "Site owner" });
            defaultRolesValues.Add(new Role { Name = "Poster", Description = "Recruiter, looking for employ personal" });
            defaultRolesValues.Add(new Role { Name = "User", Description = "Employee, looking for a job" });

            modelBuilder.Entity<Role>().HasData(defaultRolesValues);
            modelBuilder.Entity<JobType>().HasData(defaultJobTypes);
        }

    }
}
