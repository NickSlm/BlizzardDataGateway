namespace DataGateway.Services.Interfaces
{
    public interface ICacheService
    {


        Task<string?> GetStringAsync(string key);
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
        Task RemoveAsync(string key);

    }
}
