using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class ClientFileDao : IClientDao
{
    private readonly FileContext context;
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

    public ClientFileDao(FileContext context, GrpcContext grpcContext)
    {
        this.context = context;
        psc = grpcContext.Psc;
    }

    public Task<Client> CreateAsync(Client client)
    {
        context.Clients.Add(client);
        context.SaveChanges();

        return Task.FromResult(client);
    }

    public Task<Client?> GetByIdAsync(string id)
    {
        PersistentServerClient.Client grpcClient = psc.GetClientById(new PersistentServerClient.ClientBasicDTO { ClientId = long.Parse(id) });
        return Task.FromResult(new Client { id = grpcClient.ClientId, name = grpcClient.Name, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = grpcClient.PlanType });
    }

    public Task<IEnumerable<Client>> GetAsync(SearchClientParametersDto searchParameters)
    {
        IEnumerable<Client> existing = context.Clients.AsEnumerable();
        if (!string.IsNullOrEmpty(searchParameters.id))
        {
            existing  = existing.Where(c => c.identityDocument.Equals(searchParameters.id, StringComparison.OrdinalIgnoreCase));
        }
        return Task.FromResult(existing);
    }
}