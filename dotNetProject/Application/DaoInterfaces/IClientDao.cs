using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IClientDao
{
    Task<Client> CreateAsync(Client client);
    Task<Client?> GetByIdAsync(long id);

    Task<IEnumerable<Client>> GetAsync(SearchClientParametersDto searchParameters);
}