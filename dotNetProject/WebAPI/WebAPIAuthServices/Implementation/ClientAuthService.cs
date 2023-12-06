using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using FileData;
using FileData.DAOs;
using PersistentServerClient;
using Client = Domain.Model.Client;

namespace WebAPI.WebAPIAuthServices;

public class ClientAuthService : IClientAuthService
{
    private readonly IClientLogic ClientLogic;
    private IGrpcClientServices _clientServices = new GrpcClientServices(new GrpcContext());

    public ClientAuthService(IClientLogic clientLogic)
    {
        this.ClientLogic = clientLogic;
    }
    public async Task<Client> GetClientAsync(string username, string password)
    {
        Client client = await ClientLogic.GetByUsernameAsync(username);
        return client;
    }

    public Task RegisterUser(Client client)
    {
        if (string.IsNullOrEmpty(client.username))
        {
            throw new ValidationException("username cannot be null");
        }

        if (string.IsNullOrEmpty(client.password))
        {
            throw new ValidationException("password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        return Task.CompletedTask;    }

    public async Task<Client> ValidateClient(Client client)
    {
        Client? clientExisting = await _clientServices.GetById(client.id);
        
        if (clientExisting == null)
        {
            throw new Exception("Client not found");
        }

        if (!clientExisting.password!.Equals(client.password))
        {
            throw new Exception("password mismatch");
        }

        return clientExisting;
    }
}