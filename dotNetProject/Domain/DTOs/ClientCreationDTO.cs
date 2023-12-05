using Domain.Model;

namespace Domain.DTOs;

public class ClientCreationDTO
{
    public string firstname { get; set; }
    
    public string lastname { get; set; }
    
    public string username { get; set; }
    
    public string password { get; set; }
    public string country { get; set; }
    public string identityDocument { get; set; }
    public string birthday { get; set; }
    public string planType { get; set; }

    public ClientCreationDTO(string firstname,string lastname, string username, string password, string country, string identityDocument, string birthday, string planType)
    {
        this.firstname = firstname;
        this.lastname = lastname;
        this.username = username;
        this.password = password;
        this.country = country;
        this.identityDocument = identityDocument;
        this.birthday = birthday;
        this.planType = planType;
    }

    public ClientCreationDTO()
    { }
}