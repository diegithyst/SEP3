namespace Domain.DTOs;

public class AdministratorBasicDTO
{
    public string userName {  get; set; }
    public string password { get; set; }

    public AdministratorBasicDTO(string username, string password)
    {
       this.userName = username;
       this.password = password;
    }
}