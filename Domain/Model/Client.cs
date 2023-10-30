namespace Domain.Model;

public class Client
{
    public string name { get; set; }
    public string country { get; set; }
    public string identityDocument { get; set; }
    public string birthday { get; set; }
    public string planType { get; set; }
    private int id { get; set; }
}