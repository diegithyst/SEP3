namespace Domain.DTOs;

public class SearchTransferParametersDto
{
    public long? senderAccountId { get; set; }
    public long? receiverAccountId { get; set; }

    public SearchTransferParametersDto(long? senderAccountId, long? receiverAccountId)
    {
        this.senderAccountId = senderAccountId;
        this.receiverAccountId = receiverAccountId;
    }
}