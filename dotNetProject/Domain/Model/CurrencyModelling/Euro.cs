namespace Domain.Model;

public class Euro:ICurrency
{
    public string name { get; set; }
    public double balance { get; set; }
    public long id { get; set; }
    public long acountId { get; set; }

    public Euro()
    {
        name = "Euro";
    }

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

    public Krone ConvertToKrone()
    {
        throw new NotImplementedException();
    }

    public Krone convertToKrone()
    {
        return new Krone
        {
            balance = 7.4561332 * balance
        };
    }
}