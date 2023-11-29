using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IClientService
{
    Task<ICollection<Client>> GetAsync();
    
    Task<Client> GetByIdAsync(long id);

    Task DeleteAsync(long id);
    
    Task UpdateAsync();
    public Task RegisterAsync(ClientCreationDTO dto);
}