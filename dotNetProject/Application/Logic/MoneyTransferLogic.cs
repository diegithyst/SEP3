using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class MoneyTransferLogic : IMoneyTransferLogic
{
    private IMoneyTransferDao _moneyTransferDao;
    private IGrpcAccountServices _accountGrpcServices;
    private IAccountLogic _accountLogic;

    public MoneyTransferLogic(IMoneyTransferDao _moneyTransferDao, IGrpcAccountServices accountGrpcServices, IAccountLogic accountLogic)
    {
        this._moneyTransferDao = _moneyTransferDao;
        this._accountGrpcServices = accountGrpcServices;
        this._accountLogic = accountLogic;
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
        Account accountRecipient = await _accountGrpcServices.GetById(transfer.accountNumberRecipient);
        Account accountSender = await _accountGrpcServices.GetById(transfer.accountNumberSender);
        
        _accountLogic.UpdateBalanceAsync(accountSender, -dto.Value, dto.Currency);
        _accountLogic.UpdateBalanceAsync(accountRecipient, dto.Value, dto.Currency);
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