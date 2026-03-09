using DataGateway.Data;
using DataGateway.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataGateway.Services
{
    public class AuthService: IAuthService
    {

        private readonly UsersDbContext _dbContext;


        public AuthService(UsersDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public async Task<(bool success, string? errorMessage)> SaveUser(string username, string password)
        {

            var user = await _dbContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (user != null)
            {
                return (false, $"Username {username} already exists!");
            }

            return (true, null);
        }
    }
}
