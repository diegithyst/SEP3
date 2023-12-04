using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class AdministratorLogic : IAdministratorLogic
{
    private readonly IAdministratorDao adminServices;

    public AdministratorLogic(IAdministratorDao adminServices)
    {
        this.adminServices = adminServices;
    }


    public async Task<Administrator> CreateAsync(AdministratorCreationDTO adminToCreate)
    {
        Administrator? existing = await adminServices.GetByUsernameAsync(adminToCreate.username);
        if (existing != null)
        {
            throw new Exception("There is already an administrator with the same username");
        }

        Administrator toCreate = new Administrator
        {
            username = adminToCreate.username,
            password = adminToCreate.password,
            emailDomain = "admin"
        };
        Administrator created = await adminServices.CreateAsync(toCreate);
        return created;
    }

    public Task<Administrator?> GetByUsernameAsync(string username)
    {
        return adminServices.GetByUsernameAsync(username);
    }

    public Task<Administrator?> GetByIdAsync(long id)
    {
        return adminServices.GetByIdAsync(id);
    }
}