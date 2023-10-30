using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class ClientLogic : IClientLogic
{
    private readonly IClientDao _clientDao;

    public ClientLogic(IClientDao clientDao)
    {
        _clientDao = clientDao;
    }


    public async Task<Client> CreateAsync(ClientCreationDTO clientToCreate)
    {
        Client? existing = await _clientDao.GetByIdAsync(clientToCreate.id);

        if (existing != null)
        {
            throw new Exception("There is already a client with this ID!");
        }

        //ValidateData(clientToCreate);
        //TODO what restrictions do we want?
        Client toCreate = new Client
        {
            name = clientToCreate.name,
            country = clientToCreate.name,
            identityDocument = clientToCreate.id,
            birthday = clientToCreate.birthday,
            planType = clientToCreate.planType
        };

        Client created = await _clientDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<Client?>> GetAsync(SearchClientParametersDto searchClientParametersDto)
    {
        return _clientDao.GetAsync(searchClientParametersDto);
    }


    public Task<Client?> GetByIdAsync(string searchById)
    {
        return _clientDao.GetByIdAsync(searchById);
    }
}