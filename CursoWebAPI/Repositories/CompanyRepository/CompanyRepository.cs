using EmploymentExchange.Data;
using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Repositories
{
    public class CompanyRepository : ICompany
    {
        private readonly MyDBContext dbContext;

        public CompanyRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await dbContext.Companies
                .Where(e => e.State)
                .ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid id)
        {
            Company? companie = await dbContext.Companies
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

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
                .FirstOrDefaultAsync(e => e.Id == id);

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
                .FirstOrDefaultAsync(e => e.Id == id);

            if (companyExist == null) return null;

            companyExist.State = false;
            await dbContext.SaveChangesAsync();

            return companyExist;
        }
    }
}
