using Domain.Model;

namespace Application.DaoInterfaces;

public interface IAccountDao
{
    Task<Account> CreateAsync(Account account);
    Task<IEnumerable<Account>> GetByOwnerIdAsync(long ownerId);
    Task<Account> GetByIdAsync(long id);
    Task TransferMoneyAsync(double amountToTranfer, int accountIdentifier);
    Task UpdateBalanceAsync(Account account);

}