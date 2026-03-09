using Microsoft.EntityFrameworkCore;

namespace DataGateway.Data
{
    public class UsersDbContext: DbContext
    {


        public UsersDbContext(DbContextOptions<UsersDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Username)
                .IsUnique();

        }

    }
}
