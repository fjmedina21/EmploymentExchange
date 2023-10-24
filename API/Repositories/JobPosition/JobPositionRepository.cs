using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class JobPositionRepository : IJobPosition
    {
        private readonly MyDBContext dbContext;
        private readonly IMapper mapper;

        public JobPositionRepository(MyDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IQueryable<JobPosition> LoadData() => dbContext.JobPositions.Where(e => e.State)
            .Where(e => e.Category.State).Include(e => e.Category);


        public async Task<APIResponse> GetAllAsync(string? category)
        {

            IQueryable<JobPosition> entities = LoadData()!.OrderBy(e => e.Name);
            int total = entities.Count();

            if (!string.IsNullOrWhiteSpace(category))
            {
                entities = entities.Where(e => e.Category.Name.Equals(category));
                total = entities.Count();
            }

            List<JobPosition> result = await entities.ToListAsync();
            List<GetJobPositionDTO> dto = mapper.Map<List<GetJobPositionDTO>>(entities);

            return new APIResponse(Data: dto, Total: total);
        }

        public async Task<APIResponse> GetByIdAsync(Guid id)
        {
            JobPosition? entity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            GetJobPositionDTO dto = mapper.Map<GetJobPositionDTO>(entity);
            return entity is not null ? new APIResponse(Data: dto) : new APIResponse(StatusCode: 404);
        }

        public async Task<APIResponse> CreateAsync(JobPositionDTO dto)
        {
            if (await dbContext.Categories.FindAsync(dto.CategoryId) is null)
                return new APIResponse(StatusCode: 400, Message: "Category doesn't exist ");

            JobPosition entity = mapper.Map<JobPosition>(dto);

            var entry = await dbContext.JobPositions.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            GetJobPositionDTO created = mapper.Map<GetJobPositionDTO>(entry.Entity);
            return new APIResponse(StatusCode: 201, Data: created);
        }

        public async Task<APIResponse> UpdateAsync(Guid id, JobPositionDTO dto)
        {
            JobPosition entity = mapper.Map<JobPosition>(dto);
            JobPosition? dbEntity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbEntity is null) return new APIResponse(StatusCode: 400);

            entity.Id = id;
            dbContext.Attach(entity);

            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            entry.Property(e => e.CreatedAt).IsModified = false;

            await dbContext.SaveChangesAsync();

            GetJobPositionDTO updated = mapper.Map<GetJobPositionDTO>(entry.Entity);
            return new APIResponse(Data: updated);
        }

        public async Task<APIResponse> DeleteAsync(Guid id)
        {
            JobPosition? entity = await LoadData().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (entity is null) return new APIResponse(StatusCode: 404);

            entity.State = false;
            await dbContext.SaveChangesAsync();

            return new APIResponse(StatusCode: 204);
        }
    }
}
