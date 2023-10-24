using API.Models;

namespace API.Repositories
{
    public interface ICategory
    {
        Task<APIResponse> GetCategoriesAsync();
        Task<APIResponse> GetCategoryByIdAsync(Guid id);
        Task<APIResponse> CreateCategoryAsync(CategoryDTO dto);
        Task<APIResponse> UpdateCategoryAsync(Guid id, CategoryDTO dto);
        Task<APIResponse> DeleteCategoryAsync(Guid id);
    }
}
