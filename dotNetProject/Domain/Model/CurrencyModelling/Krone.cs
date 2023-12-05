namespace Domain.Model;

public class Krone:ICurrency
{
    public string name { get; set; } = "Krone";
    public double balance { get; set; } = 0;
    public long accountId { get; set; }

    public Euro convertToEuro()
    {
        return new Euro
        {
            balance = 0.1341225 * balance
        };
    }

    public Pound convertToPound()
    {
        return convertToEuro().convertToPound();
    }

    public Krone convertToKrone()
    {
        return this;
    }
}