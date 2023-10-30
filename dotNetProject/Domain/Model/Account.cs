using System.Runtime.CompilerServices;

namespace Domain.Model;

public class Account
{
    public long id { get; set; }
    public string mainCurrency { get; set; }
    public bool loan { get; set; }
    public double balance { get; set; }
    public long ownerId { get; set; }
}