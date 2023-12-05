using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;
using System.Diagnostics.Metrics;

namespace FileData.DAOs;

public class GrpcClientServices : IGrpcClientServices
{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

    public GrpcClientServices(GrpcContext grpcContext)
    {
        psc = grpcContext.Psc;
    }
    
    public Task<Client> Create(ClientCreationDTO dto)
    {
        PersistentServerClient.Client grpcClient = psc.CreateClient(new PersistentServerClient.ClientCreationDTO { UserName = dto.username, FirstName = dto.firstname, LastName = dto.lastname, Password = dto.password, Country = dto.country, Birthday = dto.birthday, IdentityDocument = dto.identityDocument, PlanType = dto.planType });
        
        return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName,lastname = grpcClient.LastName,username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = new DefaultPlan() });
    }

    public Task<Client?> GetById(long id)
    {
        PersistentServerClient.Client grpcClient = psc.GetClientById(new PersistentServerClient.ClientBasicDTO { ClientId = id });
        IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
        if (grpcClient == null)
        {
            return null;
        }
        else
        {
            return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName,lastname = grpcClient.LastName,username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });

        }
    }

    public Task<Client?> GetByUsername(string username)
    {
        PersistentServerClient.Client grpcClient = psc.GetClientByUsername(new PersistentServerClient.ClientUsernameDTO { Username = username });
        IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
        if (grpcClient == null)
        {
            return null;
        }
        else
        {
            return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName, lastname = grpcClient.LastName, username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });

        }
    }

    public Task Update(ClientCreationDTO client)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Client>> GetClients()
    {
        List<Client> clients = new List<Client>();
        PersistentServerClient.GrpcClients call = psc.GetClients(new PersistentServerClient.Empty { });
        foreach (PersistentServerClient.Client grpcClient in call.Clients)
        {
            IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
            clients.Add(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName,lastname = grpcClient.LastName,username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });
        }

        return Task.FromResult(clients.AsEnumerable());
    }

    public Task Update(ClientUpdateDTO client)
    {
        
        PersistentServerClient.Client gc = psc.UpdateClient(new PersistentServerClient.ClientUpdateDTO { FirstName = client.firstname, LastName = client.lastname, UserName = client.username, Password = client.password, Country = client.country,Birthday = client.birthday, IdentityDocument = client.identityDocument, PlanType = client.planType });
        if (gc != null)
        {
            return Task.CompletedTask;
        }
        throw new Exception("Update  failed.");
    }
}