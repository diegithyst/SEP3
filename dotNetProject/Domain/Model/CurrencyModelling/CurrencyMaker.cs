namespace Domain.Model;

public class CurrencyMaker
{
    public static ICurrency MakeCurrency(string currencyString)
    {
        switch (currencyString.ToLower())
        {
            case "euro":
                return new Euro();
            case "pound":
                return new Pound();
            case "krone":
                return new Krone();
        }

        return new Euro();
    }
}