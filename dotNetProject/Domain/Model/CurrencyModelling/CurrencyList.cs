namespace Domain.Model;

public class CurrencyList
{
    ICollection<ICurrency> Currencies = new List<ICurrency>();

    public CurrencyList()
    {
        ICurrency euro = new Euro();
        ICurrency pound = new Pound();
        ICurrency krone = new Krone();
        Currencies.Add(euro);
        Currencies.Add(pound);
        Currencies.Add(krone);
    }
}