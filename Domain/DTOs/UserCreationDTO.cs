namespace Domain.DTOs;

public class UserCreationDTO
{
    public string username { get; set; }
    public string password { get; set; }

    public UserCreationDTO(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}