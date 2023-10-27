namespace Domain.Models;

public class Comment
{
    public string text { get; set; }
    public User author { get; }
    public Post post { get; }
    public int id { get; set; }
    public int upvote { get; set; }
    public int downVote { get; set; }

    public Comment(string text, User author, Post post)
    {
        this.text = text;
        this.author = author;
        this.post = post;
        upvote = 0;
        downVote = 0;
    }
}