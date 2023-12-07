using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class AccountLogic : IAccountLogic
{
    private readonly IGrpcAccountServices accountServices;
    private readonly IGrpcClientServices _clientDao;

    public AccountLogic(IGrpcAccountServices accountServices, IGrpcClientServices clientDao)
    {
        this.accountServices = accountServices;
        _clientDao = clientDao;
    }

    public async Task<Account> CreateAsync(AccountCreationDTO dto)
    {
        //This needs updating depending whether the dto has loan true or false
        //Loan true has only one currency and also a value of loan taken, so this needs to be pushed into the currencies entity
        Client? existing = await _clientDao.GetById(dto.ownerId);
        if (existing == null)
        {
            throw new Exception($"no client was found with the id {dto.ownerId}!");
        }
        Account account = new Account
        {
            name = dto.name,
            mainCurrency = dto.mainCurrency,
            loan = dto.loan,
            ownerId = dto.ownerId,
            Euro = CurrencyMaker.MakeCurrencyWithValue("Euro", dto.mainCurrency.ToLower().Equals("euro") && dto.loan ? dto.value : 0),
            Krone = CurrencyMaker.MakeCurrencyWithValue("Krone", dto.mainCurrency.ToLower().Equals("krone") && dto.loan ? dto.value : 0),
            Pound = CurrencyMaker.MakeCurrencyWithValue("Pound", dto.mainCurrency.ToLower().Equals("pound") && dto.loan ? dto.value : 0)
        };
        Account created = await accountServices.Create(account);
        return created;
    }

    public Task<IEnumerable<Account?>> GetByOwnerIdAsync(long ownerId)
    {
        return accountServices.GetByOwnerId(ownerId);
    }

    public Task<Account?> GetByIdAsync(long id)
    {
        if (accountServices.GetById(id) == null)
        {
            throw new Exception("there is no account with that id");
        }
        return accountServices.GetById(id);
    }

    public async Task UpdateBalanceAsync(Account account, double amount, string currency)
    {
        if (currency.Equals("Euro"))
        {
            account.Euro.balance += amount;
        }
        if (currency.Equals("Pound"))
        {
            account.Pound.balance += amount;
        }
        if (currency.Equals("Krone"))
        {
            account.Krone.balance += amount;
        }

        AccountUpdateDTO dto = new AccountUpdateDTO
        {
             id = account.id,clientId = account.ownerId,name = account.name, mainCurrency = account.mainCurrency, euro = account.Euro.balance, krone = account.Krone.balance,
            pound = account.Pound.balance
        };

        await accountServices.UpdateAccount(dto);
    }
    public Task<Boolean> DeleteAsync(long id)
    {
        return accountServices.Delete(id);
    }
}
