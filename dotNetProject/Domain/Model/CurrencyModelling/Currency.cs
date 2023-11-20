namespace Domain.Model;

public interface Currency
{
    public string name { get; set; }
    public double balance { get; set; }
    public long id { get; set; }
    public long acountId { get; set; }
}