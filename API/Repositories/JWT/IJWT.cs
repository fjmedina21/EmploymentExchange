using API.Models;

namespace API.Repositories
{
    public interface IJWT
    {
        public Task<string> CreateJWTAsync(User user);
        public (Guid, List<string>) DecodeJWT(string token);
    }
}
