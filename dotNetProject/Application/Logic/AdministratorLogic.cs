using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class AdministratorLogic : IAdministratorLogic
{
    private readonly IAdministratorDao adminDao;

    public AdministratorLogic(IAdministratorDao adminDao)
    {
        this.adminDao = adminDao;
    }


    public async Task<Administrator> CreateAsync(AdministratorCreationDTO adminToCreate)
    {
        Administrator? existing = await adminDao.GetByUsernameAsync(adminToCreate.username);
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
        Administrator created = await adminDao.CreateAsync(toCreate);
        return created;
    }

    public Task<Administrator?> GetByUsernameAsync(string username)
    {
        return adminDao.GetByUsernameAsync(username);
    }

    public Task<Administrator?> GetByIdAsync(long id)
    {
        return adminDao.GetByIdAsync(id);
    }
}