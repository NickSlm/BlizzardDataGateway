using Microsoft.EntityFrameworkCore;
using Tracker.Data;

namespace DataGateway.Services
{
    public class DbService
    {

        private MyDbContext _dbContext;

        public DbService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DateTime> GetSnapshot()
        {
            var snapshot = await _dbContext.LeaderboardSnapshots.Where(s => s.Id == 5).FirstOrDefaultAsync();

            return snapshot.DatePulled;
        }

    }
}
