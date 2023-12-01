namespace Domain.DTOs;

public class AdministratorCreationDTO
{
    public string username { get; set; }
    public string password { get; set; }

    public AdministratorCreationDTO(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}