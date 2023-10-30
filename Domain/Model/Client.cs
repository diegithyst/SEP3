namespace Domain.Model;

public class Client
{
    public string name { get; set; }
    public string country { get; set; }
    public string identityDocument { get; set; }
    public DateTime birthday { get; set; }
    public string planType { get; set; }
    private int id { get; set; }
}