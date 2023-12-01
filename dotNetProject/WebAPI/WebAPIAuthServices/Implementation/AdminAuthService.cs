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
        Administrator admin = await adminLogic.GetByUsername(username);
        return admin;
    }
}