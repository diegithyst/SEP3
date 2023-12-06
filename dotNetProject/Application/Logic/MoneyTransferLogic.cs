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
            amount = dto.Amount,
            currency = dto.SenderCurrency
        };
        Account accountRecipient = await accountServices.GetById(transfer.accountNumberRecipient);
        Account accountSender = await accountServices.GetById(transfer.accountNumberSender);
        
        _accountLogic.UpdateBalanceAsync(accountSender, -dto.Amount, dto.SenderCurrency);
        _accountLogic.UpdateBalanceAsync(accountRecipient, dto.Amount, dto.SenderCurrency);
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