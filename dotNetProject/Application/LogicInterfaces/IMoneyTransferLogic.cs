using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IMoneyTransferLogic
{
    Task<MoneyTransfer> CreateAsync(MoneyTransferCreationDto dto);
    Task<IEnumerable<MoneyTransfer?>> GetAsync();
    Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(SearchTransferParametersDto dto);
}