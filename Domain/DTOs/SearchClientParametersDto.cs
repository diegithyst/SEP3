namespace Domain.DTOs;

public class SearchClientParametersDto
{
    public string? id { get; set; }

    public SearchClientParametersDto(string? id)
    {
        this.id = id;
    }
}