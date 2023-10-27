namespace Domain.DTOs;

public class PostEditDto
{
    public int postId { get; set; }
    public int ownerId { get; set; }
    public string? newTitle { get; set; }
    public string? newBody { get; set; }

    public PostEditDto(int postId, int ownerId, string? newTitle, string? newBody)
    {
        this.postId = postId;
        this.ownerId = ownerId;
        this.newTitle = newTitle;
        this.newBody = newBody;
    }
}