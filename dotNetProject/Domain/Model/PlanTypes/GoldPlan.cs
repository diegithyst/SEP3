namespace Domain.Model;

public class GoldPlan:IPlan
{
    public string name { get; }
    public double interestRate { get;}
    public double loanRate { get; }

    public GoldPlan()
    {
        name = "Gold";
        interestRate = 0.05;
        loanRate = 0.1;
    }


    public string getName()
    {
        return name;
    }

    public double getInterestRate()
    {
        return interestRate;
    }

    public double getLoanRate()
    {
        return loanRate;
    }
}