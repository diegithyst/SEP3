using Domain.Model;

namespace Application.DaoInterfaces;

public interface IAccountDao
{
    Task<Account> CreateAsync(Account account);
    Task<IEnumerable<Account>> GetByOwnerIdAsync(string ownerId);
    Task<Account> GetByIdAsync(int id);
    Task TransferMoneyAsync(double amountToTranfer, int accountIdentifier);

}