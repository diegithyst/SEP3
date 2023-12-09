namespace BlazorWASM.Data;

public class SilverPlan : IPlanBlazor
{
    public string name { get; init; } = "Silver";
    public string description { get; init; } = $"You get all the benefits from the previous plan!\nPlus, you get 0,02 interest rate and 0,08 loan rate!";
    public double price { get; init; } = 4.9;
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