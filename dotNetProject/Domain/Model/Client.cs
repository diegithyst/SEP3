namespace Domain.Model;

public class Client
{
    public string name { get; set; }
    public string country { get; set; }
    public string identityDocument { get; set; }
    public string birthday { get; set; }
    public IPlan planType { get; set; }
    public long id { get; set; }
}