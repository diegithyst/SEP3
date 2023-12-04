using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IGrpcAccountServices
{
    Task<Account> Create(AccountCreationDTO accountCreationDTO);
    Task<IEnumerable<Account>> GetByOwnerId(long ownerId);
    Task<Account> GetById(long id);
    Task TransferMoney(MoneyTransferCreationDto dto);
    Task UpdateAccount(AccountUpdateDTO accountUpdateDTO);
}