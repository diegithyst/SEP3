namespace Domain.Model;

public class BronzePlan : IPlan
{

    public string name { get; }
    public double interestRate { get;}
    public double loanRate { get; }

    public BronzePlan()
    {
        name = "Bronze";
        interestRate = 0.01;
        loanRate = 0.10;
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