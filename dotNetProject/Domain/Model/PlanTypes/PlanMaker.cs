namespace Domain.Model;

public static class PlanMaker
{
    
    public static IPlan MakePlan(string planString)
    {
        string real = planString.ToLower();
        switch (real)
        {
            case "default":
                return new DefaultPlan();
            case "bronze":
                return new BronzePlan();
            case "silver":
                return new SilverPlan();
            case "gold":
                return new GoldPlan();
        }

        return new DefaultPlan();
    }
}