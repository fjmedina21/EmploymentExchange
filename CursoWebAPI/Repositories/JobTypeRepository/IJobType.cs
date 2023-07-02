﻿using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IJobType
    {
        Task<List<JobType>> GetJobTypesAsync();
        Task<JobType?> GetJobTypeByIdAsync(Guid id);
        Task<JobType> CreateJobTypeAsync(JobType jobType);
        Task<JobType?> UpdateJobTypeAsync(Guid id, JobType jobType);
        Task<JobType?> DeleteJobTypeAsync(Guid id);
    }
}
