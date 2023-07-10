using EmploymentExchangeAPI.Data;
using EmploymentExchangeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchangeAPI.Repositories
{
    public class CompanyRepository : ICompany
    {
        private readonly MyDBContext dbContext;

        public CompanyRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Company>, int)> GetCompaniesAsync(int pageNumber = 1, int pageSize = 50)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<Company> companies = dbContext.Companies.AsNoTracking()
                .Where(e => e.State)
                .AsQueryable();

            List<Company> result = await companies.Skip(skipResults).Take(pageSize).ToListAsync();
            int total = companies.Count();

            return (result, total);
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid id)
        {
            Company? companie = await dbContext.Companies.AsNoTracking()
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return companie == null ? null : companie;
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            await dbContext.Companies.AddAsync(company);
            await dbContext.SaveChangesAsync();

            return company;
        }

        public async Task<Company?> UpdateCompanyAsync(Guid id, Company company)
        {
            Company? dbCompany = await dbContext.Companies
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbCompany == null) return null;

            dbCompany.Name = company.Name;
            dbCompany.Location = company.Location;
            dbCompany.URL = company.URL;
            dbCompany.Logo = company.Logo;
            dbCompany.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return dbCompany;
        }

        public async Task<Company?> DeleteCompanyAsync(Guid id)
        {
            Company? companyExist = await dbContext.Companies
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (companyExist == null) return null;

            companyExist.State = false;
            await dbContext.SaveChangesAsync();

            return companyExist;
        }
    }
}
