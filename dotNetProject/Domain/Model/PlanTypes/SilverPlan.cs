namespace Domain.Model;

public class SilverPlan:IPlan
{
    public string name { get; }
    public double interestRate { get;}
    public double loanRate { get; }

    public SilverPlan()
    {
        name = "Silver";
        interestRate = 0.02;
        loanRate = 0.8;
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