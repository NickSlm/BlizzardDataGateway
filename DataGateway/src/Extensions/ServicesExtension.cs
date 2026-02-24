using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.Services.Interfaces;
using Tracker.Settings;
using Tracker.Services;

namespace Tracker.Extensions
{
    public static class ServicesExtension
    {

        public static void AddAppServices(this IServiceCollection Services)
        { 
            Services.AddSingleton<IAuthService, AuthService>();
            Services.AddOptions<JwtSettings>().BindConfiguration("JwtSettings");
        }
        public static void AddDatabaseServices(this IServiceCollection Services)
        {
            Services.AddDbContextFactory<MyDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

        }
        public static void AddApiServices(this IServiceCollection Services)
        {
            Services.AddControllers();
        }
    }
}
