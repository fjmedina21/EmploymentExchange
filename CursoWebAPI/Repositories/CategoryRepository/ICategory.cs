using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface ICategory
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(Guid id, Category category);
        Task<Category?> DeleteCategoryAsync(Guid id);
    }
}
