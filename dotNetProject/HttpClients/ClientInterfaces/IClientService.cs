using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IClientService
{
    Task<ICollection<Client>> GetAsync();

    Task UpdateAsync(ClientUpdateDto dto);

    Task<Client> GetByIdAsync(int id);

    Task DeleteAsync(int id);
}