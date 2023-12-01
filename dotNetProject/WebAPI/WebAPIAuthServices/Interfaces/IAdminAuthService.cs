using Domain.Model;

namespace WebAPI.WebAPIAuthServices;

public interface IAdminAuthService
{
    Task<Administrator> GetAdminAsync(string username, string password);
}