namespace Domain.DTOs;

public class SearchTransferParametersDto
{
    public long? senderAccountId { get; set; }
    public long? receiverAccountId { get; set; }
    public long? id { get; set; }

    public SearchTransferParametersDto(long? senderAccountId, long? receiverAccountId, long? id)
    {
        this.senderAccountId = senderAccountId;
        this.receiverAccountId = receiverAccountId;
        this.id = id;
    }
}