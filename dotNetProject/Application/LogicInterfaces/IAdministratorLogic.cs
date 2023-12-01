using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IAdministratorLogic
{
    Task<Administrator> CreateAsync(AdministratorCreationDTO adminToCreate);
    Task<Administrator?> GetByUsernameAsync(string username);
    Task<Administrator?> GetByIdAsync(long id);
}
