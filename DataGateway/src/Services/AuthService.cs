using DataGateway.Data;
using DataGateway.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataGateway.Services
{
    public class AuthService: IAuthService
    {

        private readonly UsersDbContext _dbContext;
        private readonly IConfiguration _config;

        public AuthService(UsersDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
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

            var token = GenerateJWT(user);


            return (true, token);


        }


        private string GenerateJWT(User user)
        {

            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var signingCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claim,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
