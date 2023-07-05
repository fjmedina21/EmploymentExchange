using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IJWT
    {
        public string CreateJWT(User user);
        public Task<List<string>> DecodeJWT(string token);
    }
}
