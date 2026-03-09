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

            var existingUser = await _dbContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return (false, $"Username {existingUser.Username} already exists!");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var newUser = new User
            {
                Username = username,
                Password = passwordHash
            };

            _dbContext.Users.Add(newUser);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (DbUpdateException)
            {
                return (false, $"Username {existingUser} already exists!");

            }
        }

        public async Task<(bool success, string? token)> LoginUser(string username, string password)
        {

            var user = await _dbContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (user == null)
            {
                return (false, null);
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return (false, null);
            }

            return (true, "randomToken");


        }
    }
}
