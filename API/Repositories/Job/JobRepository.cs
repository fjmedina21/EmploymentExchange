using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class JobRepository : IJob
    {
        private readonly MyDBContext dbContext;

        public JobRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Job>, int)> GetJobsAsync(int pageNumber = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<Job> jobs = LoadData().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                switch (filterOn.ToLower())
                {
                    case "category":
                        jobs = jobs.Where(e => e.JobPosition.Category.Name.Contains(filterQuery));
                        break;
                    case "position":
                        jobs = jobs.Where(e => e.JobPosition.Name.Contains(filterQuery));
                        break;
                    case "company":
                        jobs = jobs.Where(e => e.Company.Name.Contains(filterQuery));
                        break;
                    case "location":
                        jobs = jobs.Where(e => e.Company.Location.Contains(filterQuery));
                        break;
                }
            }

            List<Job> result = await jobs.Skip(skipResults).Take(pageSize).ToListAsync();
            int total = jobs.Count();

            return (result, total);
        }

        public async Task<Job?> GetJobByIdAsync(Guid id)
        {
            Job? job = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            return job is null ? null : job;
        }

        public async Task<List<Job>> GetJobsByCategoryAsync(string category, int pageNumber, int pageSize)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<Job> jobsList = LoadData().AsNoTracking();

            List<Job> jobs = await jobsList.Where(e => e.JobPosition.Category.Name.Equals(category))
                .Skip(skipResults).Take(pageSize).ToListAsync();

            return jobs;
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            await dbContext.Jobs.AddAsync(job);
            await dbContext.SaveChangesAsync();

            return await GetJobByIdAsync(job.Id);
        }

        public async Task<Job?> UpdateJobAsync(Guid id, Job job)
        {
            Job? dbJob = await LoadData().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbJob is null) return null;

            dbJob.CompanyId = job.CompanyId;
            dbJob.JobPositionId = job.JobPositionId;
            dbJob.JobTypeId = job.JobTypeId;
            dbJob.Salary = job.Salary;
            dbJob.Description = job.Description;
            dbJob.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return await GetJobByIdAsync(dbJob.Id); ;
        }

        public async Task<Job?> DeleteJobAsync(Guid id)
        {
            Job? jobExist = await LoadData().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (jobExist is null) return null;

            jobExist.State = false;
            await dbContext.SaveChangesAsync();

            return jobExist;
        }

        private IQueryable<Job> LoadData()
        {
            IQueryable<Job> jobs = dbContext.Jobs.Where(e => e.State)
               .Include(e => e.JobPosition).ThenInclude(e => e.Category)
               .Include(e => e.JobType).Include(e => e.Company)
               .Where(e => e.JobPosition.State).Where(e => e.JobType.State).Where(e => e.Company.State)
               .OrderByDescending(e => e.UpdatedAt).ThenByDescending(e => e.CreatedAt)
               .AsQueryable();

            return jobs;
        }
    }
}
