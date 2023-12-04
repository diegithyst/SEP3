namespace Domain.Model;

public interface IPlan
{
   string getName();

   double getInterestRate();

   double getLoanRate();
}