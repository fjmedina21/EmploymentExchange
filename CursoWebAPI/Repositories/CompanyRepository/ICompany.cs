using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public interface ICompany
    {
        Task<(List<Company>, int)> GetCompaniesAsync(int pageNumber, int pageSize);
        Task<Company?> GetCompanyByIdAsync(Guid id);
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company?> UpdateCompanyAsync(Guid id, Company company);
        Task<Company?> DeleteCompanyAsync(Guid id);
    }
}
