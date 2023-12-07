using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IAccountLogic
{
    Task<Account> CreateAsync(AccountCreationDTO dto);
    Task<IEnumerable<Account?>> GetByOwnerIdAsync(long ownerId);
    //Task TransferMoneyAsync(double amountToTranfer, int accountIdentifier);
    
    Task<Account?> GetByIdAsync(long id);
    Task UpdateBalanceAsync(Account account, double amount, string currency);
    Task<Boolean> DeleteAsync(long id);
}