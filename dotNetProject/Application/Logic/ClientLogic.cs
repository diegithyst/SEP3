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
        IEnumerable<Client> existing = await _clientDao.GetAsync(new SearchClientParametersDto(null,clientToCreate.identityDocument));

        if (existing != null)
        {
            throw new Exception("There is already a client with this ID!");
        }

        IPlan convertedFromString = PlanMaker.MakePlan(clientToCreate.planType);

        
        //ValidateData(clientToCreate);
        //TODO what restrictions do we want?
        Client toCreate = new Client
        {
            name = clientToCreate.name,
            country = clientToCreate.name,
            identityDocument = clientToCreate.identityDocument,
            birthday = clientToCreate.birthday,
            planType = convertedFromString
        };

        Client created = await _clientDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<Client?>> GetAsync(SearchClientParametersDto searchClientParametersDto)
    {
        return _clientDao.GetAsync(searchClientParametersDto);
    }


    public Task<Client?> GetByIdAsync(long searchById)
    {
        return _clientDao.GetByIdAsync(searchById);
    }
}