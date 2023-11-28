using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IClientService
{
    public Task CreateAsync(ClientCreationDTO dto);
    Task<ICollection<Client>> GetAsync();
    
    Task<Client> GetByIdAsync(long id);

    Task DeleteAsync(long id);
    
    Task UpdateAsync(ClientUpdateDTO updateDto);
}