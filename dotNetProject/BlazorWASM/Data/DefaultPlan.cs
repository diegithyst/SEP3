namespace BlazorWASM.Data;

public class DefaultPlan : IPlanBlazor
{
    public string name { get; init; } = "Default";
    public string description { get; init; } = $"You get free banking!\nYou get no commission on regular transactions and free exchange of currencies. With no limit!\nFinally, you get 0,005 interest rate and 0,25 loan rate!";

    public double price { get; set; } = 0.0;
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