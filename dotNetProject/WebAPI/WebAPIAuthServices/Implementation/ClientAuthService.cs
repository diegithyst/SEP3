using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace WebAPI.WebAPIAuthServices;

public class ClientAuthService : IClientAuthService
{
    private readonly IClientLogic ClientLogic;

    public ClientAuthService(IClientLogic clientLogic)
    {
        this.ClientLogic = clientLogic;
    }
    public async Task<Client> GetClientAsync(string username, string password)
    {
        Client client = await ClientLogic.GetByUsernameAsync(username);
        return client;
    }
}