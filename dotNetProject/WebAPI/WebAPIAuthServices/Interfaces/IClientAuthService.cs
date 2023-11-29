using Domain.DTOs;
using Domain.Model;

namespace WebAPI.WebAPIAuthServices;

public interface IClientAuthService
{
    Task<Client> GetClientAsync(string username, string password);
}