using API.Models;

namespace API.Repositories
{
    public interface IJobType
    {
        Task<(List<JobType>, int)> GetJobTypesAsync();
        Task<JobType?> GetJobTypeByIdAsync(Guid id);
        Task<JobType> CreateJobTypeAsync(JobType jobType);
        Task<JobType?> UpdateJobTypeAsync(Guid id, JobType jobType);
        Task<JobType?> DeleteJobTypeAsync(Guid id);
    }
}
