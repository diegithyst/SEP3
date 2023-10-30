namespace Domain.DTOs;

public class ClientCreationDTO
{
    public string name { get; set; }
    public string country { get; set; }
    public string id { get; set; }
    public DateTime birthday { get; set; }
    public string planType { get; set; }

    public ClientCreationDTO(string name, string country, string id, DateTime birthday, string planType)
    {
        this.name = name;
        this.country = country;
        this.id = id;
        this.birthday = birthday;
        this.planType = planType;
    }
}