using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IClientLogic
{
    Task<Client> CreateAsync(ClientCreationDTO clientToCreate);
    
    Task<IEnumerable<Client?>> GetAsync(SearchClientParametersDto searchClientParametersDto);
    
    Task<Client?> GetByIdAsync(string id);
}