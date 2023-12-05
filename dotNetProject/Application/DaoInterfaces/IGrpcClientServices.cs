using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IGrpcClientServices
{
    Task<Client> Create(ClientCreationDTO dto);
    Task<Client?> GetById(long id);

    Task<IEnumerable<Client>> GetClients();
    Task<Client?> GetByUsername(string username);
    Task Update(ClientUpdateDTO client);

}