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
        Client client = null;
        try
        {
            client = await clientServices.GetByIdentityDocument(clientToCreate.identityDocument);
        }
        catch (Exception ex)
        {
            //There is no existing client with this identitydocumment, which is good.
        }
        if (client != null)
        {
            throw new Exception("There is already a client with this Identity Document!");
        }
        //ValidateData(clientToCreate);
        ClientCreationDTO toCreate = new ClientCreationDTO
        {
            firstname = clientToCreate.firstname,
            lastname = clientToCreate.lastname,
            username = clientToCreate.username,
            password = clientToCreate.password,
            country = clientToCreate.country,
            identityDocument = clientToCreate.identityDocument,
            birthday = clientToCreate.birthday,
            planType = clientToCreate.planType
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

    public async Task UpdateAsync(ClientUpdateDTO updateDto)
    {
        Client? toEdit = await clientServices.GetById(updateDto.id);

        if (toEdit == null)
        {
            throw new Exception($"The client with id: {updateDto.id} doesn't exist");
        }

        string? firstnameToChange = updateDto.firstname ?? toEdit.firstname;
        string? lastnameToChange = updateDto.lastname ?? toEdit.lastname;
        string? usernameToChange = updateDto.username ?? toEdit.username;
        string? passwordToChange = updateDto.password ?? toEdit.password;
        string? countryToChange = updateDto.country ?? toEdit.country;
        string? ident = updateDto.identityDocument ?? toEdit.identityDocument;
        string? birthToCh = updateDto.birthday ?? toEdit.birthday;
        string? planToChange = updateDto.planType ?? toEdit.planType?.getName();
        

        ClientUpdateDTO updated = new ClientUpdateDTO(toEdit.id)
        {
            firstname = firstnameToChange,
            lastname = lastnameToChange,
            username = usernameToChange,
            password = passwordToChange,
            country = countryToChange,
            identityDocument = ident,
            birthday = birthToCh,
            planType = planToChange
        };
        
        await clientServices.Update(updated);
    }

    public Task<Client?> GetByUsernameAsync(string username)
    {
        return clientServices.GetByUsername(username);
    }
    public Task<Boolean> DeleteClientAsync(long id)
    {
        return clientServices.Delete(id);
    }

}