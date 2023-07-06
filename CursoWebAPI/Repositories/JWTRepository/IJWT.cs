using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IJWT
    {
        public Task<string> CreateJWTAsync(User user);
        //public Task<List<string>> DecodeJWT(string token);
    }
}
