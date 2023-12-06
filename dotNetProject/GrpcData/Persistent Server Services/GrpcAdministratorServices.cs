using System.Security.Cryptography;
using Application.DaoInterfaces;
using Domain.Model;
using PersistentServerClient;
using Client = Domain.Model.Client;

namespace FileData.DAOs;

public class GrpcAdministratorServices : IAdministratorServices

{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

    public GrpcAdministratorServices(PersistentServer.PersistentServerClient psc)
    {
        this.psc = psc;
    }

    public Task<Administrator> CreateAsync(Administrator administrator)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator?> GetByUsernameAsync(string username)
    {
        PersistentServerClient.GrpcAdministrator grpcAdmin = psc.GetAdministratorByUsername(new PersistentServerClient.AdministratorBasicDTO() { Username = username,Password = password,EmailDomain = emaildomain });
        if (grpcAdmin == null)
        {
            return null;
        }
        else
        {
            return Task.FromResult(new Administrator { username = grpcAdmin.username,emailDomain = grpcAdmin.emailDomain,password = grpcAdmin.password});

        }
    }

    public Task<Administrator?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}