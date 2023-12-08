using Domain.Model;

namespace Domain.DTOs;

public class AccountExchangeDTO
{
    public long id;
    public double amount;
    public string currencyFrom;
    public string currencyTo;
}