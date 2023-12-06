using Application.DaoInterfaces;
using Domain.Model;

namespace FileData.DAOs;

public class GrpcAdministratorServices : IAdministratorServices
{
    public Task<Administrator> CreateAsync(Administrator administrator)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator?> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<Administrator?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}