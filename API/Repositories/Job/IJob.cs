using API.Models;

namespace API.Repositories
{
    public interface IJob
    {
        Task<(List<Job>, int)> GetJobsAsync(int pageNumber, int pageSize, string? filterOn, string? filterQuery);
        Task<Job?> GetJobByIdAsync(Guid id);
        Task<List<Job>> GetJobsByCategoryAsync(string category, int pageNumber, int pageSize);
        Task<Job> CreateJobAsync(Job job);
        Task<Job?> UpdateJobAsync(Guid id, Job job);
        Task<Job?> DeleteJobAsync(Guid id);
    }
}
