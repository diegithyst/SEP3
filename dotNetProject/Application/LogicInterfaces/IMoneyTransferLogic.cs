using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IMoneyTransferLogic
{
    Task<MoneyTransfer> CreateAsync(MoneyTransferCreationDto dto);
    public Task<IEnumerable<MoneyTransfer>> GetByAccountIdAsync(long id);
    Task<MoneyTransfer?> GetByIdAsync(long id);
}