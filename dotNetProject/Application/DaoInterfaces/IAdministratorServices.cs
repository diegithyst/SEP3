using Domain.Model;

namespace Application.DaoInterfaces;

public interface IAdministratorServices
{
    Task<Administrator?> GetByUsernameAsync(string username);
}