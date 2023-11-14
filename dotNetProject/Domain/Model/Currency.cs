namespace Domain.Model;

public class Currency
{
    public string name { get; set; }
    public double balance { get; set; }
    public long id { get; set; }
    public long acountId { get; set; }
}