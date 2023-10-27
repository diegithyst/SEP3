namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? username { get; }
    public int? userId { get; }
    public bool? edited { get; }
    public string? titleContains { get; }
    public string? bodyContains { get; }

    public SearchPostParametersDto(string? username, int? userId, bool? edited, string? titleContains, string? bodyContains)
    {
        this.username = username;
        this.userId = userId;
        this.edited = edited;
        this.titleContains = titleContains;
        this.bodyContains = bodyContains;
    }
}