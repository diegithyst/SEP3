namespace BlazorWASM.Data;

public class GoldPlan : IPlanBlazor
{
    public string name = "Gold";
    public string description { get; init; } =
        $"You get all the benefits from the previous plan!\nPlus, you get 0,05 interest rate and 0,1 loan rate!";

    public double price = 7.49;
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