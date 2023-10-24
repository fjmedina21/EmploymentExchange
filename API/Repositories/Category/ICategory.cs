using API.Models;

namespace API.Repositories
{
    public interface ICategory
    {
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> GetByIdAsync(Guid id);
        Task<APIResponse> CreateAsync(CategoryDTO dto);
        Task<APIResponse> UpdateAsync(Guid id, CategoryDTO dto);
        Task<APIResponse> DeleteAsync(Guid id);
    }
}
