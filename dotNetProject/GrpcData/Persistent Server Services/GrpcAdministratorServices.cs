using Application.DaoInterfaces;
using Domain.Model;
using Domain.DTOs;

namespace FileData.DAOs;

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
            psc.GetAdminByUsername(new PersistentServerClient.AdministratorUsernameDTO{Username = username});
        if (grpcAdministrator == null)
        {
            return null;
        }
        else
        {
            return Task.FromResult(new Administrator
                { username = grpcAdministrator.Username, password = grpcAdministrator.Username, emailDomain = grpcAdministrator.Domain});
        }
    }
}