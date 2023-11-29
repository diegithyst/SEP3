using System.Security.Claims;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IAuthService
{
    public Task LoginAdminAsync(string username, string password);
    public Task LoginClientAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}