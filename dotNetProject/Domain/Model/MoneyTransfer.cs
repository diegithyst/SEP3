namespace Domain.Model;

public class MoneyTransfer
{
 private long accountNumberSender { get; set; }
 
 private long accountNumberRecipient { get; set; }
 
 private ICurrency currency { get; set; }
 
 private double value { get; set; }
}