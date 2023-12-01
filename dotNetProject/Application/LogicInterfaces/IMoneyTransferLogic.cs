using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IMoneyTransferLogic
{
    Task<MoneyTransfer> CreateAsync(MoneyTransferCreationDto dto);
    Task<IEnumerable<MoneyTransfer?>> GetAsync();
    Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(SearchTransferParametersDto dto);
    public Task<IEnumerable<MoneyTransfer?>?> GetByAccountIdAsync(long id);
    Task<MoneyTransfer?> GetByIdAsync(long id);
}