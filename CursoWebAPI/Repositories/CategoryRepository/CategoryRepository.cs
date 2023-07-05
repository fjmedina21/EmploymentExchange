using EmploymentExchange.Data;
using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly MyDBContext dbContext;

        public CategoryRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await dbContext.Categories.AsNoTracking()
                .OrderBy(e => e.Name)
                .Where(e => e.State)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            Category? category = await dbContext.Categories.AsNoTracking()
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return category == null ? null : category;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(Guid id, Category category)
        {
            Category? dbCatgory = await dbContext.Categories
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbCatgory == null) return null;

            dbCatgory.Name = category.Name;
            dbCatgory.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return dbCatgory;
        }

        public async Task<Category?> DeleteCategoryAsync(Guid id)
        {
            Category? categoryExist = await dbContext.Categories
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (categoryExist == null) return null;

            categoryExist.State = false;
            await dbContext.SaveChangesAsync();

            return categoryExist;
        }
    }
}
