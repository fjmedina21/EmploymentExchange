using API.Models;

namespace API.Repositories
{
    public interface IJobPosition
    {
        Task<APIResponse> GetAllAsync(string? category = null);
        Task<APIResponse> GetByIdAsync(Guid id);
        Task<APIResponse> CreateAsync(JobPositionDTO dto);
        Task<APIResponse> UpdateAsync(Guid id, JobPositionDTO dto);
        Task<APIResponse> DeleteAsync(Guid id);
    }
}
