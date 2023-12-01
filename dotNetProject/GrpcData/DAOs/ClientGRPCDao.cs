using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class ClientGRPCDao : IClientDao
{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

    public ClientGRPCDao(GrpcContext grpcContext)
    {
        psc = grpcContext.Psc;
    }
    
    public Task<Client> CreateAsync(ClientCreationDTO clientDto)
    {
        PersistentServerClient.Client grpcClient = psc.CreateClient(clientDto);

        IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
        return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName,lastname = grpcClient.LastName,username = grpcClient.UserName, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });
    }

    public Task<Client?> GetByIdAsync(long id)
    {
        PersistentServerClient.Client grpcClient = psc.GetClientById(new PersistentServerClient.ClientBasicDTO { ClientId = id });
        IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
        if (grpcClient == null)
        {
            return null;
        }
        else
        {
            return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName,lastname = grpcClient.LastName,username = grpcClient.UserName, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });

        }
    }

    public Task<IEnumerable<Client>> GetAsync(SearchClientParametersDto searchParameters)
    {
        IEnumerable<Client> existing = psc.GetClients().AsEnumerable();
        if (!string.IsNullOrEmpty(searchParameters.identityDocument))
        {
            existing  = existing.Where(c => c.identityDocument.Equals(searchParameters.identityDocument, StringComparison.OrdinalIgnoreCase));
        }
        return Task.FromResult(existing);
    }

    public async Task UpdateAsync(Client client)
    {
        throw new NotImplementedException();
    }
}