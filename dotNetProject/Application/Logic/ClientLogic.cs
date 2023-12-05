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
        this.clientServices = clientServices;
    }


    public async Task<Client> CreateAsync(ClientCreationDTO clientToCreate)
    {
        IEnumerable<Client> existing = await clientServices.GetClients();

        if (existing != null)
        {
            foreach (var client in existing)
            {
                if (client.identityDocument.Equals(clientToCreate.identityDocument))
                {
                    throw new Exception("There is already a client with this Identity Document!");
                }
            }
        }

        //ValidateData(clientToCreate);
        //TODO what restrictions do we want?
        ClientCreationDTO toCreate = new ClientCreationDTO
        {
            firstname = clientToCreate.firstname,
            lastname = clientToCreate.lastname,
            username = clientToCreate.username,
            password = clientToCreate.password,
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


    public Task<Client?> GetByIdAsync(long searchById)
    {
        return clientServices.GetById(searchById);
    }

    public async Task UpdateAsync(ClientUpdateDTO updateDto, long id)
    {
        Client? toEdit = await clientServices.GetById(id);

        if (toEdit == null)
        {
            throw new Exception($"The client with id: {id} doesn't exist");
        }

        string firstnameToChange = updateDto.firstname ?? toEdit.firstname;
        string lastnameToChange = updateDto.lastname ?? toEdit.lastname;
        string usernameToChange = updateDto.username ?? toEdit.username;
        string passwordToChange = updateDto.password ?? toEdit.password;
        string countryToChange = updateDto.country ?? toEdit.country;
        string ident = updateDto.identityDocument ?? toEdit.identityDocument;
        string birthToCh = updateDto.birthday ?? toEdit.birthday;
        string planToChange = updateDto.planType ?? toEdit.planType.getName();

        ClientCreationDTO updated = new ClientCreationDTO(firstnameToChange, lastnameToChange, usernameToChange,
            passwordToChange, countryToChange, ident, birthToCh, planToChange);
        
        await clientServices.Update(updated);
    }

    public Task<Client?> GetByUsernameAsync(string username)
    {
        return clientServices.GetByUsername(username);
    }
}