namespace Domain.Model;

public class MoneyTransfer
{
 public long accountNumberSender { get; set; }
 
 public long accountNumberRecipient { get; set; }
 
 public Currency currency { get; set; }
 
 public double value { get; set; }
}