using Domain.Model;

namespace Domain.DTOs;

public class AccountJsonDto
{
    public string name { get; set; }
    public long id { get; set; }
    public string mainCurrency { get; set; }
    public bool loan { get; set; }
    public long ownerId { get; set;}

    public Euro euro { get; set; }
    public Pound pound { get; set; }
    public Krone krone { get; set; }


}
