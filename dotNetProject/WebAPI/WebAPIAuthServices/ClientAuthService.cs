using Application.LogicInterfaces;
using Domain.Model;

namespace WebAPI.WebAPIAuthServices;

public class ClientAuthService : IClientAuthService
{
    private readonly IClientLogic ClientLogic;

    public ClientAuthService(IClientLogic clientLogic)
    {
        ClientLogic = clientLogic;
    }
    public async Task<Client> GetClientAsync(string username, string password)
    {
        Client client = await ClientLogic.GetByUsernameAsync(username);
        if (client.password.Equals(password))
        {
            return client; 
        }
        else
        {
            throw new Exception("Password mismatch");
        }
    }
}