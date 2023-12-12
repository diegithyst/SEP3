using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class MoneyTransferLogic : IMoneyTransferLogic
{
    private IGrpcMoneyTransferServices moneyTransferServices;
    private IGrpcAccountServices accountServices;
    private IAccountLogic _accountLogic;

    public MoneyTransferLogic(IGrpcMoneyTransferServices moneyTransferServerices, IGrpcAccountServices accountDao, IAccountLogic accountLogic)
    {
        this.moneyTransferServices = moneyTransferServerices;
        this.accountServices = accountDao;
        this._accountLogic = accountLogic;
    }


    public async Task<MoneyTransfer> CreateAsync(MoneyTransferCreationDto dto)
    {
        MoneyTransferCreationDto transfer = new MoneyTransferCreationDto
        {
            SenderAccountNumber = dto.SenderAccountNumber,
            ReceiverAccountNumber = dto.ReceiverAccountNumber,
            SenderCurrency = dto.SenderCurrency,
            Amount = dto.Amount
        };
        Account accountRecipient = await accountServices.GetById(dto.ReceiverAccountNumber);
        Account accountSender = await accountServices.GetById(dto.SenderAccountNumber);
        
        await _accountLogic.UpdateBalanceAsync(accountSender, -dto.Amount, dto.SenderCurrency);
        await _accountLogic.UpdateBalanceAsync(accountRecipient, dto.Amount, dto.SenderCurrency);
        MoneyTransfer created = await moneyTransferServices.TransferMoney(transfer);
       return created;
    }

    public Task<IEnumerable<MoneyTransfer>> GetByAccountIdAsync(long accountId)
        {
        return moneyTransferServices.GetMoneyTransfers(accountId);
    }

    public Task<MoneyTransfer?> GetByIdAsync(long id)
    {
        return moneyTransferServices.GetMoneyTransferById(id);
    }
}