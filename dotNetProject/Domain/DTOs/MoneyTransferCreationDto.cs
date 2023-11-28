namespace Domain.DTOs;

public class MoneyTransferCreationDto
{
    public long SenderAccountNumber { get; set; }
    public long ReceiverAccountNumber { get; set; }
    public string Currency { get; set; }
    public double Value { get; set; }

    public MoneyTransferCreationDto(long senderAccountNumber, long receiverAccountNumber, string currency, double value)
    {
        this.SenderAccountNumber = senderAccountNumber;
        this.ReceiverAccountNumber = receiverAccountNumber;
        this.Currency = currency;
        this.Value = value;
    }
}