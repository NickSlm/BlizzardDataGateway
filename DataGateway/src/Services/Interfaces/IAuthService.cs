namespace DataGateway.Services.Interfaces
{
    public interface IAuthService
    {

        Task<(bool success, string? errorMessage)> SaveUser(string username, string password);


    }
}
