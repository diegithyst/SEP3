using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IMoneyTransferService
{
    Task CreateAsync(MoneyTransferCreationDto dto);

    Task<ICollection<MoneyTransfer>> GetListByAccountIdAsync(long id);
}