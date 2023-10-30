using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IAccountService
{
    Task<Account> GetAccount(int id);
}