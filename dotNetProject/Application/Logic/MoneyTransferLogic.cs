using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class MoneyTransferLogic : IMoneyTransferLogic
{
    private IMoneyTransferDao _moneyTransferDao;
    private IAccountDao _accountDao;

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
            currency = CurrencyMaker.MakeCurrency(dto.Currency)
        };
        Account accountRecipient = await _accountDao.GetByIdAsync(transfer.accountNumberRecipient);
        Account accountSender = await _accountDao.GetByIdAsync(transfer.accountNumberSender);
        if (transfer.currency.name.Equals("Euro"))
        {
            accountSender.Euro.balance =- transfer.value;
            accountRecipient.Euro.balance =+ transfer.value;
        }
        if (transfer.currency.name.Equals("Pound"))
        {
            accountSender.Pound.balance =- transfer.value;
            accountRecipient.Pound.balance =+ transfer.value;
        }
        if (transfer.currency.name.Equals("Krone"))
        {
            accountSender.Pound.balance =- transfer.value;
            accountRecipient.Pound.balance =+ transfer.value;
        }
        _accountDao.Update(accountSender);
        _accountDao.Update(accountRecipient);
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
    
    public Task<IEnumerable<MoneyTransfer?>?> GetByAccountIdAsync(long id)
        {
            return _moneyTransferDao.GetByAccountIdAsync(id);
        }

    public Task<MoneyTransfer?> GetByIdAsync(long id)
    {
        return _moneyTransferDao.GetByIdAsync(id);
    }
}