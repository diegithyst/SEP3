namespace Domain.DTOs;

public class SearchClientParametersDto
{
    public long? id { get; set; }
    public string? identityDocument { get; set; }

    public SearchClientParametersDto(long? id, string? indentityDocument)
    {
        this.id = id;
       this.identityDocument = indentityDocument;
    }
}