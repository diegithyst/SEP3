namespace Domain.Model;

public class MoneyTransfer
{
 private long accountNumberSender { get; set; }
 
 private long accountNumberRecipient { get; set; }
 
 private Currency currency { get; set; }
 
 private double value { get; set; }
}