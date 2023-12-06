namespace Domain.DTOs;

public class MoneyTransferCreationDto
{
    public long SenderAccountNumber { get; set; }
    public long ReceiverAccountNumber { get; set; }
    public string SenderCurrency { get; set; }
    public double Amount { get; set; }

    public MoneyTransferCreationDto(long senderAccountNumber, long receiverAccountNumber, string currency, double value)
    {
        this.SenderAccountNumber = senderAccountNumber;
        this.ReceiverAccountNumber = receiverAccountNumber;
        this.SenderCurrency = currency;
        this.Amount = value;
    }
}