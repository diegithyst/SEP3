using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class ClientLogic : IClientLogic
{
    private readonly IGrpcClientServices clientServices;

    public ClientLogic(IGrpcClientServices clientServices)
    {
        clientServices = clientServices;
    }


    public async Task<Client> CreateAsync(ClientCreationDTO clientToCreate)
    {
        IEnumerable<Client> existing = await clientServices.GetClients();

        foreach (var client in existing)
        {
            if (client.identityDocument.Equals(clientToCreate.identityDocument))
            {
                throw new Exception("There is already a client with this ID!");
            }
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

        Client created = await clientServices.Create(toCreate);

        return created;
    }

    public Task<IEnumerable<Client?>> GetAll()
    {
        return clientServices.GetClients();
    }
    
    public Task<IEnumerable<Client?>> GetBySearch(ClientBasicDTO dto)
    {
        return clientServices.GetBySearch(dto);
    }

    public Task<Client?> GetByIdAsync(long searchById)
    {
        return clientServices.GetById(searchById);
    }

    public async Task UpdateAsync(ClientUpdateDTO updateDto)
    {
        Client? toEdit = await clientServices.GetById(updateDto.id);

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
        await clientServices.Update(edited);
    }

    public Task<Client?> GetByUsernameAsync(string username)
    {
        return clientServices.GetByUsernameAsync(username);
    }
}