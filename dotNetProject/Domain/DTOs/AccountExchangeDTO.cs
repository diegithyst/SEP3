using Domain.Model;

namespace Domain.DTOs;

public class AccountExchangeDTO
{
    public long id { get; set; }
    public double amount { get; set; }
    public string currencyFrom { get; set; }
    public string currencyTo { get; set; }
}