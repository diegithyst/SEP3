namespace Domain.DTOs;

public class ClientUpdateDTO
{
    public string? username { get; set; }
    public string? firstname { get; set; }

    public string? lastname { get; set; }
    
    public string? password { get; set; }
    public string? country { get; set; }
    public string? identityDocument { get; set; }
    public string? birthday { get; set; }
    public string? planType { get; set; }
    public long id { get; init; }

    public ClientUpdateDTO(long id)
    {
        this.id = id;
    }

}