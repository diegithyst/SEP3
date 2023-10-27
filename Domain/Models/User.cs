namespace Domain.Models;

public class User
{
    public string username { get; set; }
    public string password { get; set; }

    public int id { get; set; }
    public int securityLevel { get; set;}

    public string toString()
    {
        return username;
    }

}