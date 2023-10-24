using AutoMapper;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CompanyRepository : ICompany
    {
        private readonly MyDBContext dbContext;
        private readonly IMapper mapper;


        public CompanyRepository(MyDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        private IQueryable<Company>? LoadData() => dbContext.Companies.Where(e => e.State);


        public async Task<APIResponse> GetAllAsync(int pageNumber = 1, int pageSize = 50)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<Company> entities = LoadData()!.OrderBy(e => e.CreatedAt).ThenByDescending(e => e.UpdatedAt).AsNoTracking();
            int total = entities.Count();

            List<Company> result = await entities.Skip(skipResults).Take(pageSize).ToListAsync();
            List<GetCompanyDTO> dto = mapper.Map<List<GetCompanyDTO>>(entities);

            return (new APIResponse(Data: dto, Total: total));
        }

        public async Task<APIResponse> GetByIdAsync(Guid id)
        {
            Company? entity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            GetCompanyDTO dto = mapper.Map<GetCompanyDTO>(entity);
            return entity is not null ? new APIResponse(Data: dto) : new APIResponse(StatusCode: 404);
        }

        public async Task<APIResponse> CreateAsync(CompanyDTO dto)
        {
            Company entity = mapper.Map<Company>(dto);

            var entry = await dbContext.Companies.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            GetCompanyDTO created = mapper.Map<GetCompanyDTO>(entry.Entity);
            return new APIResponse(StatusCode: 201, Data: created);
        }

        public async Task<APIResponse> UpdateAsync(Guid id, CompanyDTO dto)
        {
            Company entity = mapper.Map<Company>(dto);
            Company? dbEntity = await LoadData().AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbEntity is null) return new APIResponse(StatusCode: 400);

            entity.Id = id;
            dbContext.Attach(entity);

            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            entry.Property(e => e.CreatedAt).IsModified = false;

            await dbContext.SaveChangesAsync();

            GetCompanyDTO updated = mapper.Map<GetCompanyDTO>(entry.Entity);
            return new APIResponse(Data: updated);
        }

        public async Task<APIResponse> DeleteAsync(Guid id)
        {
            Company? entity = await LoadData().FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (entity is null) return new APIResponse(StatusCode: 404);

            entity.State = false;
            await dbContext.SaveChangesAsync();

            return new APIResponse(StatusCode: 204);
        }
    }
}
