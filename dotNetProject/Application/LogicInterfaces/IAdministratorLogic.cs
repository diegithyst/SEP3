using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IAdministratorLogic
{
    Task<Administrator?> GetByUsernameAsync(string username);
    
}
