namespace Domain.Model;

public interface ICurrency
{
    public string name { get; set; }
    public double balance { get; set; }
    public long acountId { get; set; }

    public Euro convertToEuro();
    public Pound convertToPound();
    public Krone convertToKrone();
    
}