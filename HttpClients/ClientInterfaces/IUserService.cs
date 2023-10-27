using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> CreateAsync(UserCreationDTO dto);
    Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null);
}