namespace Domain.Model;

public class Client
{
    public string? firstname { get; set; }
    
    public string? lastname { get; set; }

    public string? username { get; set; }
    public string? password { get; set; }
    public string? country { get; set; }
    public string? identityDocument { get; set; }
    public string? birthday { get; set; }
    public IPlan? planType { get; set; }
    public long id { get; set; }
    
}