using Domain.Models;

namespace Domain.DTOs;

public class PostCreationDTO
{
    public int ownerID { get; set; }
    public string title { get; set; }
    public string body { get; set; }
    
    public PostCreationDTO(int ownerID, string title, string body)
    {
        this.title = title;
        this.body = body;
        this.ownerID = ownerID;
    }
}