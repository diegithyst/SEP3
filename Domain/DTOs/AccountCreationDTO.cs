using Domain.Model;

namespace Domain.DTOs;

public class AccountCreationDTO
{
    public string mainCurrency { get; set; }
    public bool loan { get; set; }
    public string ownerId { get; set; }
}