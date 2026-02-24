using DataGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Data
{
    public class MyDbContext: DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {

        }

        public DbSet<LeaderboardSnapshot> LeaderboardSnapshots { get; set; }
        public DbSet<DataGateway.Data.LeaderboardEntry> LeaderboardEntry { get; set; } = default!;

    }
}
