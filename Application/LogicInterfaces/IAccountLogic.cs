using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IAccountLogic
{
    Task<Account> CreateAsync(AccountCreationDTO dto);
    Task<IEnumerable<Account?>> GetByOwnerIdAsync(string? ownerId);
    //Task TransferMoneyAsync(double amountToTranfer, int accountIdentifier);
    
    Task<Account?> GetByIdAsync(int id);
}