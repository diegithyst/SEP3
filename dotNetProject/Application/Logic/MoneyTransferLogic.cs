using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class MoneyTransferLogic : IMoneyTransferLogic
{
    private IMoneyTransferDao _moneyTransferDao;

    public MoneyTransferLogic(IMoneyTransferDao _moneyTransferDao)
    {
        this._moneyTransferDao = _moneyTransferDao;
    }


    public async Task<MoneyTransfer> CreateAsync(MoneyTransferCreationDto dto)
    {
        MoneyTransfer transfer = new MoneyTransfer
        {
            accountNumberRecipient = dto.ReceiverAccountNumber,
            accountNumberSender = dto.SenderAccountNumber,
            value = dto.Value,
            currency = dto.Currency;
        };
        MoneyTransfer created = await _moneyTransferDao.CreateAsync(transfer);
        return created;
    }

    public Task<IEnumerable<MoneyTransfer?>> GetAsync()
    {
        return _moneyTransferDao.GetAsync();
    }

    public Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(SearchTransferParametersDto dto)
    {
        return _moneyTransferDao.GetBySearchAsync(dto);
    }
}