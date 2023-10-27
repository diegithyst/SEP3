using Domain.Models;

namespace Domain.DTOs;

public class PostBasicDto
{
    public string title { get; set; }
    public string body { get; set; }
    public User author { get; set; }
    public bool edited { get; set; }
    public int id { get; set; }
    
    public int upVote { get; set; }
    public int downVote { get; set; }

    public PostBasicDto(User author, string title, string body, int upVote, int downVote, bool edited)
    {
        this.author = author;
        this.title = title;
        this.body = body;
        this.upVote = upVote;
        this.downVote = downVote;
        this.edited = edited;
    }

}