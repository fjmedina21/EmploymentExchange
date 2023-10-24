using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class JobTypeRepository : IJobType
    {
        private readonly MyDBContext dbContext;
        private readonly IMapper mapper;

        public JobTypeRepository(MyDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IQueryable<JobType>? LoadData() => dbContext.JobTypes.Where(e => e.State);

        public async Task<APIResponse> GetAllAsync()
        {
            IQueryable<JobType> entities = LoadData()!.OrderBy(e => e.Name);
            int total = entities.Count();

            List<JobType> result = await entities.ToListAsync();
            List<GetJobTypeDTO> dto = mapper.Map<List<GetJobTypeDTO>>(entities);

            return (new APIResponse(Data: dto, Total: total));
        }

        public async Task<APIResponse> GetByIdAsync(Guid id)
        {
            JobType? entity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            GetJobTypeDTO dto = mapper.Map<GetJobTypeDTO>(entity);
            return entity is not null ? new APIResponse(Data: dto) : new APIResponse(StatusCode: 404);
        }

        public async Task<APIResponse> CreateAsync(JobTypeDTO dto)
        {
            JobType entity = mapper.Map<JobType>(dto);

            var entry = await dbContext.JobTypes.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            GetJobTypeDTO created = mapper.Map<GetJobTypeDTO>(entry.Entity);
            return new APIResponse(StatusCode: 201, Data: created);
        }

        public async Task<APIResponse> UpdateAsync(Guid id, JobTypeDTO dto)
        {
            JobType entity = mapper.Map<JobType>(dto);
            JobType? dbEntity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbEntity is null) return new APIResponse(StatusCode: 400);

            entity.Id = id;
            dbContext.Attach(entity);

            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            entry.Property(e => e.CreatedAt).IsModified = false;

            await dbContext.SaveChangesAsync();

            GetJobTypeDTO updated = mapper.Map<GetJobTypeDTO>(entry.Entity);
            return new APIResponse(Data: updated);
        }

        public async Task<APIResponse> DeleteAsync(Guid id)
        {
            JobType? entity = await LoadData().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (entity is null) return new APIResponse(StatusCode: 404);

            entity.State = false;
            await dbContext.SaveChangesAsync();

            return new APIResponse(StatusCode: 204);
        }
    }
}
