using Domain.Auth;
using Domain.Model;

namespace Domain.DTOs;

public class AccountCreationDTO
{
    public string name { get; set; }
    public string mainCurrency { get; set; }
    public bool loan { get; set; }
    public double value { get; set; }
    public long ownerId { get; set; }


    public AccountCreationDTO()
    {
    }


    public AccountCreationDTO(string mainCurrency, bool loan, double value, long ownerId, string name)
    {
        this.mainCurrency = mainCurrency;
        this.loan = loan;
        this.value = value;
        this.ownerId = ownerId;
        this.name = name;
    }

    public AccountCreationDTO(string currency, bool loan, long ownerId, string name)
    {
        this.mainCurrency = currency;
        this.loan = loan;
        this.name = name;
        this.ownerId = ownerId;
    }
}