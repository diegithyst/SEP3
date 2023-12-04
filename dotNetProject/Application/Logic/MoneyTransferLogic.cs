using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class MoneyTransferLogic : IMoneyTransferLogic
{
    private IMoneyTransferDao moneyTransferServerice;
    private IGrpcAccountServices accountServices;
    private IAccountLogic _accountLogic;

    public MoneyTransferLogic(IMoneyTransferDao moneyTransferServerice, IGrpcAccountServices accountDao, IAccountLogic accountLogic)
    {
        this.moneyTransferServerice = moneyTransferServerice;
        this.accountServices = accountDao;
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
        Account accountRecipient = await accountServices.GetById(transfer.accountNumberRecipient);
        Account accountSender = await accountServices.GetById(transfer.accountNumberSender);
        
        _accountLogic.UpdateBalanceAsync(accountSender, -dto.Value, dto.Currency);
        _accountLogic.UpdateBalanceAsync(accountRecipient, dto.Value, dto.Currency);
        MoneyTransfer created = await moneyTransferServerice.CreateAsync(transfer);
        return created;
    }

    public Task<IEnumerable<MoneyTransfer?>> GetAsync()
    {
        return moneyTransferServerice.GetAsync();
    }

    public Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(SearchTransferParametersDto dto)
    {
        return moneyTransferServerice.GetBySearchAsync(dto);
    }
    
    public Task<IEnumerable<MoneyTransfer?>?> GetByAccountIdAsync(long id)
        {
            return moneyTransferServerice.GetByAccountIdAsync(id);
        }

    public Task<MoneyTransfer?> GetByIdAsync(long id)
    {
        return moneyTransferServerice.GetByIdAsync(id);
    }
}