using API.Models;

namespace API.Repositories
{
    public interface IJob
    {
        Task<APIResponse> GetAllAsync(int pageNumber, int pageSize, string? filterOn, string? filterQuery);
        Task<APIResponse> GetByCategoryAsync(string category, int pageNumber, int pageSize);
        Task<APIResponse> GetByIdAsync(Guid id);
        Task<APIResponse> CreateAsync(JobDTO dto);
        Task<APIResponse> UpdateAsync(Guid id, JobDTO dto);
        Task<APIResponse> DeleteAsync(Guid id);
    }
}
