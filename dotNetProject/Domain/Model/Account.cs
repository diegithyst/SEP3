using System.Runtime.CompilerServices;

namespace Domain.Model;

public class Account
{
    public string name { get; set; }
    public long id { get; set; }
    public string mainCurrency { get; set; }
    public double euro {  get; set; }
    public double krone { get; set; }
    public double pound {  get; set; }
    public bool loan { get; set; }
    public long ownerId { get; set; }
    public long accountViewId { get; set; }

    public ICurrency Euro { get; set; }
    public ICurrency Pound { get; set; }

    public ICurrency Krone { get; set; }
}
