using DispensarioMedico.Helpers;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
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
                FirstName = "Seed",
                LastName = "User",
                Email = "seeduser@domain.com",
                Password = "12345678"
            };

            PasswordHashing hashing = new();
            hashing.HashPassword(user.Password);

            List<JobType> defaultJobTypes = new()
            {
                new JobType { Name = "Full-Time" },
                new JobType { Name = "Part-Time" },
                new JobType { Name = "Contract" },
                new JobType { Name = "Internship" }
            };

            List<Role> defaultRolesValues = new()
            {
                new Role { Name = "admin", Description = "Site owner" },
                new Role { Name = "poster", Description = "Recruiter, looking for employ personal" },
                new Role { Name = "user", Description = "Employee, looking for a job" }
            };

            modelBuilder.Entity<Role>().HasData(defaultRolesValues);
            modelBuilder.Entity<JobType>().HasData(defaultJobTypes);
            modelBuilder.Entity<User>().HasData(user);
        }

    }
}
