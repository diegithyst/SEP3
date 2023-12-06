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
    
    
    public AccountCreationDTO(string currency, double value, bool loan, long ownerId, string name)
    {
        mainCurrency = currency;
        this.ownerId = ownerId;
        this.name = name;
        this.value = value;
        this.loan = loan;
        this.ownerId = ownerId;
    }
    
    public AccountCreationDTO(){}
}