using API.Models;

namespace API.Repositories
{
    public interface ICompany
    {
        Task<APIResponse> GetAllAsync(int pageNumber, int pageSize);
        Task<APIResponse> GetByIdAsync(Guid id);
        Task<APIResponse> CreateAsync(CompanyDTO dto);
        Task<APIResponse> UpdateAsync(Guid id, CompanyDTO dto);
        Task<APIResponse> DeleteAsync(Guid id);
    }
}
