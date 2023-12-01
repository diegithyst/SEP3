using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IMoneyTransferDao
{
    Task<MoneyTransfer> CreateAsync(MoneyTransfer moneyTransfer);
    Task<IEnumerable<MoneyTransfer?>> GetAsync();
    Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(SearchTransferParametersDto dto);
    Task<MoneyTransfer?> GetByIdAsync(long id);

}