namespace Domain.DTOs;

public class CommentCreationDto
{
    public string text { get; set; }
    public int authorId { get; set; }
    public int postId { get; set; }
    public CommentCreationDto(string text, int authorId, int postId)
    {
        this.text = text;
        this.authorId = authorId;
        this.postId = postId;
    }
}