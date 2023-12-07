using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IClientService
{
    public Task CreateAsync(ClientCreationDTO dto);
    Task<ICollection<ClientUpdateDTO>> GetAsync();

    Task<ClientUpdateDTO> GetByIdAsync(long id);

    Task DeleteAsync(long id);
    Task UpdateAsync(ClientUpdateDTO updateDto);
    public Task RegisterAsync(ClientCreationDTO dto);
}