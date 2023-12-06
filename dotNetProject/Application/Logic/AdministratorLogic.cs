using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class AdministratorLogic : IAdministratorLogic
{
    private readonly IAdministratorServices adminServices;

    public AdministratorLogic(IAdministratorServices adminServices)
    {
        this.adminServices = adminServices;
    }
    
    public Task<Administrator?> GetByUsernameAsync(string username)
    {
        return adminServices.GetByUsernameAsync(username);
    }
    
}