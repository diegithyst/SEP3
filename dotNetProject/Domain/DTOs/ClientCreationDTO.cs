namespace Domain.DTOs;

public class ClientCreationDTO
{
    public string name { get; set; }
    public string country { get; set; }
    public string identityDocument { get; set; }
    public string birthday { get; set; }
    public string planType { get; set; }

    public ClientCreationDTO(string name, string country, string identityDocument, string birthday, string planType)
    {
        this.name = name;
        this.country = country;
        this.identityDocument = identityDocument;
        this.birthday = birthday;
        this.planType = planType;
    }
}