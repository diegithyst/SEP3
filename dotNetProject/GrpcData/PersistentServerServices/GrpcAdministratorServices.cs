using Application.DaoInterfaces;
using Domain.Model;
using Domain.DTOs;

namespace FileData.PersistentServerServices;

public class GrpcAdministratorServices : IAdministratorServices

{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

    public GrpcAdministratorServices(GrpcContext context)
    {
        psc = context.Psc;
    }
    
    public Task<Administrator?> GetByUsernameAsync(string username)
    {
        PersistentServerClient.GrpcAdministrator grpcAdministrator =
            psc.AuthenticateAdministrator(new PersistentServerClient.GrpcAuthenticateAdministrator{Username = username});
        if (grpcAdministrator == null)
        {
            return null;
        }
        else
        {
            return Task.FromResult(new Administrator
                { username = grpcAdministrator.Username, password = grpcAdministrator.Username, emailDomain = "admin"});
        }
    }
}