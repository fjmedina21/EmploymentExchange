﻿using EmploymentExchange.Data;
using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Repositories
{
    public class JobRepository : IJob
    {
        private readonly MyDBContext dbContext;

        public JobRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Job>> GetJobsAsync(int pageNumber = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<Job> jobs = dbContext.Jobs
                .Where(e => e.State).Where(e => e.JobPosition.State)
                .Where(e => e.JobType.State).Where(e => e.Company.State)
                .OrderByDescending(e => e.UpdatedAt).ThenByDescending(e => e.CreatedAt)
                .Include(e => e.JobPosition).Include(e => e.JobPosition.Category)
                .Include(e => e.JobType).Include(e => e.Company)
                .Skip(skipResults).Take(pageSize)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                switch (filterOn.ToLower())
                {
                    case "category":
                        jobs = jobs.Where(e => e.JobPosition.Category.Name
                        .Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "cosition":
                        jobs = jobs.Where(e => e.JobPosition.Name
                        .Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "company":
                        jobs = jobs.Where(e => e.Company.Name
                       .Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "location":
                        jobs = jobs.Where(e => e.Company.Location
                       .Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
                        break;
                }
            }

            return await jobs.ToListAsync();
        }

        public async Task<Job?> GetJobByIdAsync(Guid id)
        {
            Job? job = await dbContext.Jobs
                .Where(e => e.State).Where(e => e.JobPosition.State)
                .Where(e => e.JobType.State).Where(e => e.Company.State)
                .Include(e => e.JobPosition).Include(e => e.JobPosition.Category)
                .Include(e => e.JobType).Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id);

            return job == null ? null : job;
        }

        public async Task<List<Job>?> GetJobsByCategoryAsync(string category, int pageNumber, int pageSize)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            List<Job>? jobs = await dbContext.Jobs
                .Where(e => e.State)
                .OrderByDescending(e => e.UpdatedAt).ThenByDescending(e => e.CreatedAt)
                .Include(e => e.JobPosition).Include(e => e.JobPosition.Category)
                .Include(e => e.JobType).Include(e => e.Company)
                .Where(e => e.JobPosition.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Skip(pageNumber).Take(pageSize)
                .ToListAsync();

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
            Job? dbJob = await dbContext.Jobs.Where(e => e.State).FirstOrDefaultAsync(e => e.Id == id);

            if (dbJob == null) return null;

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
            Job? jobExist = await dbContext.Jobs.Where(e => e.State).FirstOrDefaultAsync(e => e.Id == id);

            if (jobExist == null) return null;

            jobExist.State = false;
            await dbContext.SaveChangesAsync();

            return jobExist;
        }
    }
}
