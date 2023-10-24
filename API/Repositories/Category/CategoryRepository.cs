using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly MyDBContext dbContext;
        private readonly IMapper mapper;


        public CategoryRepository(MyDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IQueryable<Category>? LoadData() => dbContext.Categories.Where(e => e.State);


        public async Task<APIResponse> GetCategoriesAsync()
        {
            IQueryable<Category> entities = LoadData()!.OrderBy(e => e.Name).AsQueryable();

            List<Category> result = await entities.ToListAsync();
            List<GetCategoryDTO> dto = mapper.Map<List<GetCategoryDTO>>(entities);

            int total = entities.Count();
            return (new APIResponse(Data: dto, Total: total));
        }

        public async Task<APIResponse> GetCategoryByIdAsync(Guid id)
        {
            Category? entity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            GetCategoryDTO dto =  mapper.Map<GetCategoryDTO>(entity);
            return entity is not null ? new APIResponse(Data: dto) : new APIResponse(StatusCode: 404);
        }

        public async Task<APIResponse> CreateCategoryAsync(CategoryDTO dto)
        {
            Category entity = mapper.Map<Category>(dto);
            entity.CreatedAt = DateTime.Now;

            var entry = await dbContext.Categories.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            GetCategoryDTO created = mapper.Map<GetCategoryDTO>(entry.Entity);
            return new APIResponse(StatusCode: 201, Data: created);
        }

        public async Task<APIResponse> UpdateCategoryAsync(Guid id, CategoryDTO dto)
        {
            Category entity = mapper.Map<Category>(dto);
            Category? dbEntity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbEntity is null) return new APIResponse(StatusCode: 400);

            entity.Id = id;
            dbContext.Attach(entity);

            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            entry.Property(e => e.CreatedAt).IsModified = false;

            await dbContext.SaveChangesAsync();

            GetCategoryDTO updated = mapper.Map<GetCategoryDTO>(entry.Entity);
            return new APIResponse(Data: updated);
        }

        public async Task<APIResponse> DeleteCategoryAsync(Guid id)
        {
            Category? entity = await LoadData().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (entity is null) return new APIResponse(StatusCode: 404);

            entity.State = false;
            await dbContext.SaveChangesAsync();

            return new APIResponse(StatusCode: 204);
        }
    }
}
