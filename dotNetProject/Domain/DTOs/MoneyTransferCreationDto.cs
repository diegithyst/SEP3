namespace Domain.DTOs;

public class MoneyTransferCreationDto
{
    private long SenderAccountNumber { get; set; }
    private long ReceiverAccountNumber { get; set; }
    private string Currency { get; set; }
    private double Value { get; set; }

    public MoneyTransferCreationDto(long senderAccountNumber, long receiverAccountNumber, string currency, double value)
    {
        this.SenderAccountNumber = senderAccountNumber;
        this.ReceiverAccountNumber = receiverAccountNumber;
        this.Currency = currency;
        this.Value = value;
    }
}