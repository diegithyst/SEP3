namespace Domain.Model;

public class Euro:ICurrency
{
    public string name { get; set; } = "Euro";
    public double balance { get; set; } = 0;
    public long acountId { get; set; }

    public Euro convertToEuro()
    {
        return this;
    }

    public Pound convertToPound()
    {
        return new Pound
        {
            balance = 0.86667023 * balance
        };
    }
    
    public Krone convertToKrone()
    {
        return new Krone
        {
            balance = 7.4561332 * balance
        };
    }
}