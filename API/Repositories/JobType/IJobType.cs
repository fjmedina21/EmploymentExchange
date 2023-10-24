using API.Models;

namespace API.Repositories
{
    public interface IJobType
    {
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> GetByIdAsync(Guid id);
        Task<APIResponse> CreateAsync(JobTypeDTO dto);
        Task<APIResponse> UpdateAsync(Guid id, JobTypeDTO dto);
        Task<APIResponse> DeleteAsync(Guid id);
    }
}
