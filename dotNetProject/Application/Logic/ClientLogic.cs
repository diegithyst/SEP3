using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class ClientLogic : IClientLogic
{
    private readonly IGrpcClientServices _clientDao;

    public ClientLogic(IGrpcClientServices clientDao)
    {
        _clientDao = clientDao;
    }


    public async Task<Client> CreateAsync(ClientCreationDTO clientToCreate)
    {
        IEnumerable<Client> existing = await _clientDao.GetClients(new AdministratorBasicDTO(null,clientToCreate.identityDocument));

        if (existing != null)
        {
            throw new Exception("There is already a client with this ID!");
        }

        
        //ValidateData(clientToCreate);
        //TODO what restrictions do we want?
        ClientCreationDTO toCreate = new ClientCreationDTO
        {
            firstname = clientToCreate.firstname,
            lastname = clientToCreate.lastname,
            username = clientToCreate.username,
            country = clientToCreate.country,
            identityDocument = clientToCreate.identityDocument,
            birthday = clientToCreate.birthday,
            planType = clientToCreate.planType,
        };

        Client created = await _clientDao.Create(toCreate);

        return created;
    }

    public Task<IEnumerable<Client?>> GetAsync(AdministratorBasicDTO searchClientParametersDto)
    {
        return _clientDao.GetClients(searchClientParametersDto);
    }


    public Task<Client?> GetByIdAsync(long searchById)
    {
        return _clientDao.GetById(searchById);
    }

    public async Task UpdateAsync(ClientUpdateDTO updateDto)
    {
        Client? toEdit = await _clientDao.GetByIdAsync(updateDto.id);

        if (toEdit == null)
        {
            throw new Exception($"The client with id: {updateDto.id} doesn't exist");
        }
        
        IPlan convertedFromString = PlanMaker.MakePlan(updateDto.planType);


        Client edited = new Client
        {
            firstname = updateDto.firstname,
            lastname = updateDto.lastname,
            username = updateDto.username,
            country = updateDto.country,
            identityDocument = updateDto.identityDocument,
            birthday = updateDto.birthday,
            planType = convertedFromString
        };
        await _clientDao.UpdateAsync(edited);
    }

    public Task<Client?> GetByUsernameAsync(string username)
    {
        return _clientDao.GetByUsernameAsync(username);
    }
}