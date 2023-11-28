namespace Domain.Model;

public class Krone:ICurrency
{
    public string name { get; set; }
    public double balance { get; set; }
    public long id { get; set; }
    public long acountId { get; set; }

    public Krone()
    {
        name = "Krone";
    }

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