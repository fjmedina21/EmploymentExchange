using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class JobRepository : IJob
    {
        private readonly MyDBContext dbContext;
        private readonly IMapper mapper;

        public JobRepository(MyDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IQueryable<Job> LoadData() => dbContext.Jobs.Where(e => e.State)
              .Include(e => e.JobPosition).ThenInclude(e => e.Category)
              .Include(e => e.JobType).Include(e => e.Company)
              .Where(e => e.JobPosition.State).Where(e => e.JobType.State).Where(e => e.Company.State)
              .OrderByDescending(e => e.UpdatedAt).ThenByDescending(e => e.CreatedAt);

        public async Task<APIResponse> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? filterOn = null, string? filterQuery = null)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<Job> entities = LoadData().AsNoTracking();
            int total = entities.Count();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                switch (filterOn.ToLower())
                {
                    case "category":
                        entities = entities.Where(e => e.JobPosition.Category.Name.Contains(filterQuery));
                        break;
                    case "position":
                        entities = entities.Where(e => e.JobPosition.Name.Contains(filterQuery));
                        break;
                    case "company":
                        entities = entities.Where(e => e.Company.Name.Contains(filterQuery));
                        break;
                    case "location":
                        entities = entities.Where(e => e.Company.Location.Contains(filterQuery));
                        break;
                }
            }

            List<Job> result = await entities.Skip(skipResults).Take(pageSize).ToListAsync();
            List<GetJobDTO> dto = mapper.Map<List<GetJobDTO>>(entities);

            return new APIResponse(Data: dto, Total: total);
        }

        public async Task<APIResponse> GetByCategoryAsync(string category, int pageNumber, int pageSize)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<Job> entities = LoadData().AsNoTracking().Where(e => e.JobPosition.Category.Name.Equals(category));
            int total = entities.Count();

            List<Job> result = await entities.Skip(skipResults).Take(pageSize).ToListAsync();
            List<GetJobDTO> dto = mapper.Map<List<GetJobDTO>>(entities);

            return (new APIResponse(Data: dto, Total: total));
        }

        public async Task<APIResponse> GetByIdAsync(Guid id)
        {
            Job? entity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            GetJobDTO dto = mapper.Map<GetJobDTO>(entity);
            return entity is not null ? new APIResponse(Data: dto) : new APIResponse(StatusCode: 404);
        }

        public async Task<APIResponse> CreateAsync(JobDTO dto)
        {
            Job entity = mapper.Map<Job>(dto);

            var entry = await dbContext.Jobs.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            GetJobDTO created = mapper.Map<GetJobDTO>(entry.Entity);
            return new APIResponse(StatusCode: 201, Data: created);
        }

        public async Task<APIResponse> UpdateAsync(Guid id, JobDTO dto)
        {
            Job entity = mapper.Map<Job>(dto);
            Job? dbEntity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbEntity is null) return new APIResponse(StatusCode: 400);

            entity.Id = id;
            dbContext.Attach(entity);

            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            entry.Property(e => e.CreatedAt).IsModified = false;

            await dbContext.SaveChangesAsync();

            GetJobDTO updated = mapper.Map<GetJobDTO>(entry.Entity);
            return new APIResponse(Data: updated);
        }

        public async Task<APIResponse> DeleteAsync(Guid id)
        {
            Job? entity = await LoadData().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (entity is null) return new APIResponse(StatusCode: 404);

            entity.State = false;
            await dbContext.SaveChangesAsync();

            return new APIResponse(StatusCode: 204);
        }
    }
}
