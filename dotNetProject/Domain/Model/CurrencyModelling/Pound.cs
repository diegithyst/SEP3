namespace Domain.Model;

public class Pound : ICurrency
{
    
    public string name { get; set; }
    public double balance { get; set; }
    public long id { get; set; }
    public long acountId { get; set; }

    public Pound()
    {
        name = "Pound";
    }

    public Euro convertToEuro()
    {
        return new Euro
        {
            balance = 1.1537862 * balance
        };
    }
    
    public Pound convertToPound()
    {
        return this;
    }

    public Krone convertToKrone()
    {
        return convertToEuro().convertToKrone();
    }
}