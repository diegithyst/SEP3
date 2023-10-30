using System.Runtime.CompilerServices;

namespace Domain.Model;

public class Account
{
    public string mainCurrency { get; set; }
    public bool loan { get; set; }
    public double balance { get; set; }
    public string ownerId { get; set; }
    public int identifier { get; set; }

    public Account(string mainCurrency, bool loan, string ownerId)
    {
        this.mainCurrency = mainCurrency;
        this.loan = loan;
        this.ownerId = ownerId;
    }
}