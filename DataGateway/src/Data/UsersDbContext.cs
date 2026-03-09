using Microsoft.EntityFrameworkCore;

namespace DataGateway.Data
{
    public class UsersDbContext: DbContext
    {


        public UsersDbContext(DbContextOptions<UsersDbContext> options): base(options)
        {

        }

        DbSet<User> Users { get; set; }

    }
}
