using DataGateway.Data;
using DataGateway.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Tracker.Data;

namespace DataGateway.Services
{
    public class DbService: IDbService
    {
        private readonly ICacheService _cacheService;
        private readonly MyDbContext _dbContext;

        public DbService(MyDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public async Task<List<LeaderboardSnapshot>> ListSnapshots()
        {
            var snapshots = await _dbContext.LeaderboardSnapshots
                .ToListAsync();

            return snapshots;
        }
        public async Task<LeaderboardSnapshot> GetSnapshotByDate(DateTime date)
        {
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

        public async Task<List<LeaderboardEntry>> GetTop10Entries()
        {
            string cacheKey = "top10Players";

            var cache = await _cacheService.GetStringAsync(cacheKey);
            if (cache != null)
            {
                var cachedTop10 = JsonSerializer.Deserialize<List<LeaderboardEntry>>(cache);
                return cachedTop10;
            }


            var top10 = await _dbContext.LeaderboardEntry
                        .GroupBy(e => e.CharacterId)
                        .Select(g => new LeaderboardEntry { CharacterId = g.Key,
                            CharacterName = g.First().CharacterName,
                            Rank = g.First().Rank,
                            Won = g.First().Won,
                            Lost = g.First().Lost,
                            Played = g.First().Played,
                            Rating = g.Max(e => e.Rating) })
                        .OrderByDescending(g => g.Rating)
                        .Take(10)
                        .ToListAsync();

            var json = JsonSerializer.Serialize(top10);

            await _cacheService.SetAsync(cacheKey, json, TimeSpan.FromMinutes(10));


            return top10;

        }


    }
}
