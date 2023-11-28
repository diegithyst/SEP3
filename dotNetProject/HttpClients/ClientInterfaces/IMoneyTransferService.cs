using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IMoneyTransferService
{
    Task CreateAsync(MoneyTransferCreationDto dto);
    Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(long? receiverAccount, long? senderAccount, long? id);
    Task<IEnumerable<MoneyTransfer?>?> GetAsync();
}