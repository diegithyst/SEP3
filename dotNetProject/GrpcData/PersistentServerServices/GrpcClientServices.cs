using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;
using System.Diagnostics.Metrics;

namespace FileData.PersistentServerServices;

public class GrpcClientServices : IGrpcClientServices
{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

    public GrpcClientServices(GrpcContext grpcContext)
    {
        psc = grpcContext.Psc;
    }
    
    public Task<Client> Create(ClientCreationDTO dto)
    {
        PersistentServerClient.Client grpcClient = psc.CreateClient(new PersistentServerClient.ClientCreationDTO { UserName = dto.username, FirstName = dto.firstname, LastName = dto.lastname, Password = dto.password, Country = dto.country, Birthday = dto.birthday, IdentityDocument = dto.identityDocument, PlanType = new DefaultPlan().getName() });
        
        return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName,lastname = grpcClient.LastName,username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = new DefaultPlan() });
    }

    public Task<Client?> GetById(long id)
    {
        PersistentServerClient.Client grpcClient = psc.GetClientById(new PersistentServerClient.ClientBasicDTO { ClientId = id });
        
        if (grpcClient == null)
        {
            return null;
        }
        else
        {
            IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
            return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName,lastname = grpcClient.LastName,username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });

        }
    }

    public Task<Client?> GetByUsername(string username)
    {
        PersistentServerClient.Client grpcClient = psc.GetClientByUsername(new PersistentServerClient.ClientUsernameDTO { Username = username });
        if (grpcClient == null)
        {
            return null;
        }
        else
        {
            IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
            return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName, lastname = grpcClient.LastName, username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });

        }
    }
    public Task<Client?> GetByIdentityDocument(string identityDocument)
    {
        PersistentServerClient.Client grpcClient = psc.GetClientByIdentityDocument(new PersistentServerClient.ClientIdentityDocumentDTO { IdentityDocument = identityDocument });
        if (grpcClient == null)
        {
            return null;
        }
        else
        {
            IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
            return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName, lastname = grpcClient.LastName, username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });

        }
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

    public Task<Client> Update(ClientUpdateDTO client)
    {
        Console.WriteLine(client.id);
        PersistentServerClient.Client grpcClient = psc.UpdateClient(new PersistentServerClient.ClientUpdateDTO { FirstName = client.firstname, LastName = client.lastname, UserName = client.username, Password = client.password, Country = client.country,Birthday = client.birthday, IdentityDocument = client.identityDocument, PlanType = client.planType, ClientId = client.id});
        if (grpcClient != null)
        {
            IPlan newPlan = PlanMaker.MakePlan(grpcClient.PlanType);
            return Task.FromResult(new Client { id = grpcClient.ClientId, firstname = grpcClient.FirstName, lastname = grpcClient.LastName, username = grpcClient.UserName, password = grpcClient.Password, country = grpcClient.Country, birthday = grpcClient.Birthday, identityDocument = grpcClient.IdentityDocument, planType = newPlan });
        }
        throw new Exception("Update  failed.");
    }
    public Task<Boolean> Delete(long id)
    {
        PersistentServerClient.GrpcResult result = psc.DeleteClient(new PersistentServerClient.ClientBasicDTO { ClientId = id });
        return Task.FromResult(result.Success);
    }
}