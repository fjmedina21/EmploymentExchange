using EmploymentExchangeAPI.Data;
using EmploymentExchangeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchangeAPI.Repositories
{
    public class JobPositionRepository : IJobPosition
    {
        private readonly MyDBContext dbContext;

        public JobPositionRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<JobPosition>, int)> GetJobPositionsAsync(string? category)
        {
            IQueryable<JobPosition> jobPosition = dbContext.JobPositions.AsNoTracking()
                .Where(e => e.State).Where(e => e.Category.State)
                .Include(e => e.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                jobPosition = jobPosition
                    .Where(e => e.Category.Name.Equals(category));
            }

            List<JobPosition> result = await jobPosition.ToListAsync();
            int total = jobPosition.Count();

            return (result, total);
        }

        public async Task<JobPosition?> GetJobPositionByIdAsync(Guid id)
        {
            JobPosition? jobPosition = await dbContext.JobPositions.AsNoTracking()
                .Where(e => e.State).Where(e => e.Category.State)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return jobPosition is null ? null : jobPosition;
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
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbJobPosition is null) return null;

            dbJobPosition.Name = jobPosition.Name;
            dbJobPosition.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return await GetJobPositionByIdAsync(dbJobPosition.Id); ;
        }

        public async Task<JobPosition?> DeleteJobPositionAsync(Guid id)
        {
            JobPosition? jobPositionExist = await dbContext.JobPositions
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (jobPositionExist is null) return null;

            jobPositionExist.State = false;
            await dbContext.SaveChangesAsync();

            return jobPositionExist;
        }
    }
}
