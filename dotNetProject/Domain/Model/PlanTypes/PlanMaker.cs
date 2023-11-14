namespace Domain.Model;

public static class PlanMaker
{
    
    public static IPlan MakePlan(string planString)
    {
        switch (planString)
        {
            case "Default":
                return new DefaultPlan();
            case "Bronze":
                return new BronzePlan();
            case "Silver":
                return new SilverPlan();
            case "Gold":
                return new GoldPlan();
        }

        return new DefaultPlan();
    }
}