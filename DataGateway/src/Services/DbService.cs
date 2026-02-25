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
            //TODO: ADD check if not null
            var snapshots = await _dbContext.LeaderboardSnapshots
                .ToListAsync();

            return snapshots;
        }
        public async Task<LeaderboardSnapshot> GetSnapshotByDate(DateTime date)
        {
            //TODO: ADD check if not null
            var snapshot = await _dbContext.LeaderboardSnapshots
                .Where(s => s.DatePulled.Date == date.Date)
                .FirstOrDefaultAsync();

            return snapshot;
        }

        public async Task<List<LeaderboardEntry>> GetSnapshotEntries(int snapshotId)
        {
            var entriesList = await _dbContext.LeaderboardEntry
                .Where(e => e.SnapshotId == snapshotId)
                .ToListAsync();

            return entriesList;
        }
        public async Task<LeaderboardEntry> GetCharacter(string characterName)
        {
            var character = await _dbContext.LeaderboardEntry.Where(e => e.CharacterName == characterName).FirstOrDefaultAsync();
            return character;

        }


    }
}
