namespace Domain.DTOs;

public class ClientUpdateDTO
{
    public string firstname { get; set; }

    public string lastname { get; set; }

    public string username { get; set; }

    public string password { get; set; }
    public string country { get; set; }
    public string identityDocument { get; set; }
    public string birthday { get; set; }
    public string planType { get; set; }
    public long id { get; set; }

    public ClientUpdateDTO(string firstname, string lastname, string username, string password, string country,
        string identityDocument, string birthday, string planType, long id)
    {
        this.firstname = firstname;
        this.lastname = lastname;
        this.username = username;
        this.password = password;
        this.country = country;
        this.identityDocument = identityDocument;
        this.birthday = birthday;
        this.planType = planType;
        this.id = id;
    }
}