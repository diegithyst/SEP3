using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IClientLogic
{
    Task<Client> CreateAsync(ClientCreationDTO clientToCreate);
    
    Task<IEnumerable<Client?>> GetAll();
    Task<IEnumerable<Client?>> GetBySearch(ClientBasicDTO dto);
    
    
    Task<Client?> GetByIdAsync(long id);
    Task UpdateAsync(ClientUpdateDTO updateDto);
    Task<Client?> GetByUsernameAsync(string username);
}