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
        
        Client? existing = await _clientDao.GetById(dto.ownerId);
        if (existing == null)
        {
            throw new Exception($"no client was found with the id {dto.ownerId}!");
        }

        if (dto.loan)
        {
            Account created = new Account();
            created.mainCurrency = dto.mainCurrency;
            created.loan = dto.loan;
            created.name = dto.name;
            created.ownerId = dto.ownerId;
            created.Euro = new Euro();
            created.Krone = new Krone();
            created.Pound = new Pound();
            switch (dto.mainCurrency.ToLower())
            {
                case "krone":
                    created.Krone.balance = dto.value;
                    created.Pound.balance = 0;
                    created.Euro.balance = 0;
                    break;
                case "pound":
                    created.Krone.balance = 0;
                    created.Pound.balance = dto.value;
                    created.Euro.balance = 0;
                    break;
                case "euro":
                    created.Krone.balance = 0;
                    created.Pound.balance = 0;
                    created.Euro.balance = dto.value;
                    break;
                
            }
            return await accountServices.Create(created);
        }
        else
        {
            Account created = new Account();
            created.mainCurrency = dto.mainCurrency;
            created.loan = dto.loan;
            created.name = dto.name;
            created.ownerId = dto.ownerId;
            created.Euro = new Euro();
            created.Krone = new Krone();
            created.Pound = new Pound();
            created.Krone.balance = 0;
            created.Pound.balance = 0;
            created.Euro.balance = 0;
            return await accountServices.Create(created);
        }
    }

    public async Task<IEnumerable<Account?>> GetByOwnerIdAsync(long ownerId)
    {
        return await accountServices.GetByOwnerId(ownerId);
    }

    public async Task<Account?> GetByIdAsync(long id)
    {
        if (accountServices.GetById(id) == null)
        {
            throw new Exception("there is no account with that id");
        }
        return await accountServices.GetById(id);
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
            name = account.name, mainCurrency = account.mainCurrency, euro = account.Euro.balance, krone = account.Krone.balance,
            pound = account.Pound.balance
        };

        await accountServices.UpdateAccount(dto);
    }
}
