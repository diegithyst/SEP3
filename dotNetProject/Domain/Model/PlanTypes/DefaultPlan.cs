namespace Domain.Model;

public class DefaultPlan:IPlan
{
    public string name { get; }
    public double interestRate { get;}
    public double loanRate { get; }

    public DefaultPlan()
    {
        name = "Default";
        interestRate = 0.005;
        loanRate = 0.25;
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