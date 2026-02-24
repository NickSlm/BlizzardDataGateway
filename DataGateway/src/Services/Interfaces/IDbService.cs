using DataGateway.Data;

namespace DataGateway.Services.Interfaces
{
    public interface IDbService
    {

        Task<List<LeaderboardSnapshot>> ListSnapshots();

    }
}
