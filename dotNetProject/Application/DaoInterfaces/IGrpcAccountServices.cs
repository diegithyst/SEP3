using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IGrpcAccountServices
{
    Task<Account> Create(Account accountCreationDTO);
    Task<IEnumerable<Account>> GetByOwnerId(long ownerId);
    Task<Account> GetById(long id);
    Task TransferMoney(MoneyTransferCreationDto dto);
    Task UpdateAccount(AccountUpdateDTO accountUpdateDTO);
    Task GetMoneyTransferById(long id);
    Task<IEnumerable<MoneyTransfer>> GetMoneyTransfers(long accountId);
}