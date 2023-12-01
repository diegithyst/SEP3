using Application.DaoInterfaces;
using Domain.Model;

namespace FileData.DAOs;

public class AdministratorGRPCDao : IAdministratorDao
{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

    public AdministratorGRPCDao(GrpcContext grpcContext)
    {
        psc = grpcContext.Psc;
    }

    public Task<Administrator> CreateAsync(Administrator administrator)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator?> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}