using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public interface IJobPosition
    {
        Task<(List<JobPosition>, int)> GetJobPositionsAsync(string? category = null);
        Task<JobPosition?> GetJobPositionByIdAsync(Guid id);
        Task<JobPosition> CreateJobPositionAsync(JobPosition jobPosition);
        Task<JobPosition?> UpdateJobPositionAsync(Guid id, JobPosition jobPosition);
        Task<JobPosition?> DeleteJobPositionAsync(Guid id);
    }
}
