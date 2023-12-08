using Application.LogicInterfaces;
using Domain.Model;

namespace WebAPI.WebAPIAuthServices;

public class AdminAuthService : IAdminAuthService
{

    private readonly IAdministratorLogic adminLogic;

    public AdminAuthService(IAdministratorLogic administratorLogic)
    {
        adminLogic = administratorLogic;
    }
    public async Task<Administrator> GetAdminAsync(string username, string password)
    {
        Administrator admin = await adminLogic.GetByUsernameAsync(username);
        if (admin.password.Equals(password))
        {
            return admin; 
        }
        else
        {
            throw new Exception("Password mismatch");
        }
    }
}