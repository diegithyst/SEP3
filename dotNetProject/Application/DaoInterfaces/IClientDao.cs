using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IClientDao
{
    Task<Client> CreateAsync(ClientCreationDTO clientDto);
    Task<Client?> GetByIdAsync(long id);

    Task<IEnumerable<Client>> GetAsync(SearchClientParametersDto searchParameters);
    Task UpdateAsync(Client client);
    Task<Client> GetByUsernameAsync(string username);
}