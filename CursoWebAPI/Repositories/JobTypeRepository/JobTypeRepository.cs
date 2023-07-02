﻿using EmploymentExchange.Data;
using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Repositories
{
    public class JobTypeRepository : IJobType
    {
        private readonly MyDBContext dbContext;

        public JobTypeRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<JobType>> GetJobTypesAsync()
        {
            return await dbContext.JobTypes
                .OrderBy(e => e.Name)
                .Where(e => e.State)
                .ToListAsync();
        }

        public async Task<JobType?> GetJobTypeByIdAsync(Guid id)
        {
            JobType? jobType = await dbContext.JobTypes
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            return jobType == null ? null : jobType;
        }

        public async Task<JobType> CreateJobTypeAsync(JobType jobType)
        {
            await dbContext.JobTypes.AddAsync(jobType);
            await dbContext.SaveChangesAsync();

            return jobType;
        }

        public async Task<JobType?> UpdateJobTypeAsync(Guid id, JobType jobType)
        {
            JobType? dbJobType = await dbContext.JobTypes
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbJobType == null) return null;

            dbJobType.Name = jobType.Name;
            dbJobType.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return dbJobType;
        }

        public async Task<JobType?> DeleteJobTypeAsync(Guid id)
        {
            JobType? jobTypeExist = await dbContext.JobTypes
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (jobTypeExist == null) return null;

            jobTypeExist.State = false;
            await dbContext.SaveChangesAsync();

            return jobTypeExist;
        }
    }
}
