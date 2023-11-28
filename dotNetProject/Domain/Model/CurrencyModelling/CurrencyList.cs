namespace Domain.Model;

public class CurrencyList
{
    ICollection<ICurrency> Currencies = new List<ICurrency>();

    public CurrencyList(long accountId)
    {
        ICurrency euro = new Euro
        {
            acountId = accountId
        };
        ICurrency pound = new Pound
        {
            acountId = accountId
        };
        ICurrency krone = new Krone
        {
            acountId = accountId
        };
        Currencies.Add(euro);
        Currencies.Add(pound);
        Currencies.Add(krone);
    }
}