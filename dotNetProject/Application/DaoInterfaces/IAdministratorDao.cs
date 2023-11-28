using Domain.Model;

namespace Application.DaoInterfaces;

public interface IAdministratorDao
{
    Task<Administrator> CreateAsync(Administrator administrator);
    Task<Administrator?> GetByUsernameAsync(string username);
    Task<Administrator?> GetByIdAsync(long id);
}