using EmploymentExchangeAPI.Data;
using EmploymentExchangeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchangeAPI.Helpers
{
    public class Validators
    {
        private readonly MyDBContext dbContext;


        public Validators(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> EmailExist(string email)
        {
            User? user = await dbContext.Users.AsNoTracking().FirstAsync(e => e.Email.Equals(email));
            return (user is not null);
        }

        //IdExist
        public bool IdExist<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        //NameExist
        public bool NameExist(string name)
        {
            throw new NotImplementedException();
        }
    }
}
