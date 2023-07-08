using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface ICompany
    {
        Task<List<Company>> GetCompaniesAsync(int pageNumber, int pageSize);
        Task<Company?> GetCompanyByIdAsync(Guid id);
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company?> UpdateCompanyAsync(Guid id, Company company);
        Task<Company?> DeleteCompanyAsync(Guid id);
    }
}
