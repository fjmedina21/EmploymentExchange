using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IJob
    {
        Task<List<Job>> GetJobsAsync(int pageNumber = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null);
        Task<Job?> GetJobByIdAsync(Guid id);
        Task<List<Job>?> GetJobsByCategoryAsync(string category, int pageNumber = 1, int pageSize = 20);
        Task<Job> CreateJobAsync(Job job);
        Task<Job?> UpdateJobAsync(Guid id, Job job);
        Task<Job?> DeleteJobAsync(Guid id);
    }
}
