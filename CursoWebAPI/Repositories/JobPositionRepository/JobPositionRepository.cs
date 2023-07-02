using EmploymentExchange.Data;
using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmploymentExchange.Repositories
{
    public class JobPositionRepository : IJobPosition
    {
        private readonly MyDBContext dbContext;

        public JobPositionRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<JobPosition>> GetJobPositionsAsync(string? category)
        {
            IQueryable<JobPosition> jobPosition = dbContext.JobPositions
                .Where(e => e.State).Where(e => e.Category.State)
                .Include(e => e.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                jobPosition = jobPosition
                    .Where(e => e.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return await jobPosition.ToListAsync();
        }

        public async Task<JobPosition?> GetJobPositionByIdAsync(Guid id)
        {
            JobPosition? jobPosition = await dbContext.JobPositions
                .Where(e => e.State).Where(e => e.Category.State)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            return jobPosition == null ? null : jobPosition;
        }

        public async Task<JobPosition> CreateJobPositionAsync(JobPosition jobPosition)
        {
            await dbContext.JobPositions.AddAsync(jobPosition);
            await dbContext.SaveChangesAsync();

            return await GetJobPositionByIdAsync(jobPosition.Id);
        }

        public async Task<JobPosition?> UpdateJobPositionAsync(Guid id, JobPosition jobPosition)
        {
            JobPosition? dbJobPosition = await dbContext.JobPositions
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbJobPosition == null) return null;

            dbJobPosition.Name = jobPosition.Name;
            dbJobPosition.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return await GetJobPositionByIdAsync(dbJobPosition.Id); ;
        }

        public async Task<JobPosition?> DeleteJobPositionAsync(Guid id)
        {
            JobPosition? jobPositionExist = await dbContext.JobPositions
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (jobPositionExist == null) return null;

            jobPositionExist.State = false;
            await dbContext.SaveChangesAsync();

            return jobPositionExist;
        }
    }
}
