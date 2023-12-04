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
        accountServices = accountServices;
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
        Account created = await accountServices.Create(dto);
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

        await accountServices.Update(account);
    }
}
