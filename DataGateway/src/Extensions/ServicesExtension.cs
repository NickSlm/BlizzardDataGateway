using Microsoft.EntityFrameworkCore;
using DataGateway.Services;
using DataGateway.Services.Interfaces;
using StackExchange.Redis;
using DataGateway.Data;
using DataGateway.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DataGateway.Extensions
{
    public static class ServicesExtension
    {

        public static void AddAppServices(this IServiceCollection Services)
        { 
            Services.AddScoped<IDbService, DbService>();
            Services.AddOptions<JwtSettings>().BindConfiguration("JwtSettings");
            Services.AddScoped<ICacheService, CacheService>();
            Services.AddScoped<IAuthService, AuthService>();
        }
        public static void AddDatabaseServices(this IServiceCollection Services)
        {
            Services.AddDbContextFactory<MyDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddDbContext<UsersDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseSqlite(configuration.GetConnectionString("UsersConnection"));
            });
            Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration["Redis:ConnectionString"];

                return ConnectionMultiplexer.Connect(connectionString);
            });
        }
        public static void AddApiServices(this IServiceCollection Services)
        {
            Services.AddControllers();
        }
        public static void AddAuthenticationService(this IServiceCollection Services, IConfiguration configuration)
        {

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ValidateLifetime = true,
                };
            });
        }
    }
}
