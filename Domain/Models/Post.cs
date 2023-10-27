namespace Domain.Models;

public class Post
{
    public string title { get; set; }
    public string body { get; set; }
    public User author { get; set; }
    public bool edited { get; set; }
    public int id { get; set; }
    
    public int upVote { get; set; }
    public int downVote { get; set; }

    public Post(User author, string title, string body)
    {
        this.author = author;
        this.title = title;
        this.body = body;
        upVote = 0;
        downVote = 0;
        edited = false;
    }
    
}