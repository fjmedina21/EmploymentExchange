using DispensarioMedico.Helpers;
using EmploymentExchangeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchangeAPI.Data
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

            User user = new()
            {
                FirstName = "Seed", LastName = "User",
                Email = "seeduser@domain.com", Password = "12345678"
            };

            PasswordHashing hashing = new();
            hashing.HashPassword(user.Password);

            List<JobType> defaultJobTypes = new List<JobType>();
            defaultJobTypes.Add(new JobType { Name = "Full-Time" });
            defaultJobTypes.Add(new JobType { Name = "Part-Time" });
            defaultJobTypes.Add(new JobType { Name = "Contract" });
            defaultJobTypes.Add(new JobType { Name = "Internship" });

            List<Role> defaultRolesValues = new List<Role>();
            defaultRolesValues.Add(new Role { Name = "admin", Description = "Site owner" });
            defaultRolesValues.Add(new Role { Name = "poster", Description = "Recruiter, looking for employ personal" });
            defaultRolesValues.Add(new Role { Name = "user", Description = "Employee, looking for a job" });

            modelBuilder.Entity<Role>().HasData(defaultRolesValues);
            modelBuilder.Entity<JobType>().HasData(defaultJobTypes);
            modelBuilder.Entity<User>().HasData(user);
        }

    }
}
