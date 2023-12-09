namespace BlazorWASM.Data;

public class BronzePlan : IPlanBlazor
{
    public string name { get; init; } = "Bronze";
    public string description { get; init; } = $"You get all the benefits from the previous plan!\nPlus, you get 0,01 interest rate and 0,10 loan rate!";
    public double price { get; init; } = 2.9;
    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }

    public double getPrice()
    {
        return price;
    }
}