using DataGateway.Data;
using DataGateway.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;

namespace DataGateway.Services
{
    public class DbService: IDbService
    {

        private MyDbContext _dbContext;

        public DbService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LeaderboardSnapshot>> ListSnapshots()
        {
            var snapshots = await _dbContext.LeaderboardSnapshots.ToListAsync();
            return snapshots;
        }

    }
}
