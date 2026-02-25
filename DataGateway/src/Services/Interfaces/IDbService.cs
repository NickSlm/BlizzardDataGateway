using DataGateway.Data;

namespace DataGateway.Services.Interfaces
{
    public interface IDbService
    {

        Task<List<LeaderboardSnapshot>> ListSnapshots();
        Task<LeaderboardSnapshot> GetSnapshotByDate(DateTime date);
        Task<List<LeaderboardEntry>> GetSnapshotEntries(int snapshotId);
        Task<LeaderboardEntry> GetCharacter(string characterName);
    }
}
