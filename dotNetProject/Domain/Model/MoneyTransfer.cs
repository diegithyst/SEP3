namespace Domain.Model;

public class MoneyTransfer
{
 public long accountNumberSender { get; set; }
 
 public long accountNumberRecipient { get; set; }
 
 public string currency { get; set; }
 
 public double amount { get; set; }
 public long id { get; set; }
}