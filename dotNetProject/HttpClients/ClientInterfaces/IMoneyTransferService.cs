using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IMoneyTransferService
{
    Task CreateAsync(MoneyTransferCreationDto dto);
    Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(long? receiverAccount, long? senderAccount);
    Task<IEnumerable<MoneyTransfer?>?> GetAsync();
    Task<MoneyTransfer?> GetByIdAsync(long id);
    
    Task<ICollection<MoneyTransfer>> GetListByAccountIdAsync(long id);
    
}