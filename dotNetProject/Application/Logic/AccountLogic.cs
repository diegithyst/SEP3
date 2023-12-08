using System.Transactions;
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

    public Task<IEnumerable<Account?>> GetByClientIdAsync(long ownerId)
    {
        return accountServices.GetByClientId(ownerId);
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
        if (currency.ToLower().Equals("euro"))
        {
            account.Euro.balance += amount;
        }
        if (currency.ToLower().Equals("pound"))
        {
            account.Pound.balance += amount;
        }
        if (currency.ToLower().Equals("krone"))
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

    public async Task Exchange(long id, double amount, string currencyFrom, string currencyTo)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            try
            { 
                var accountExchange = await GetByIdAsync(id);
            var negativeAmount = (-1) * (amount);
            UpdateBalanceAsync(accountExchange, negativeAmount, currencyFrom);
            ICurrency euro = new Euro();
            ICurrency krone = new Krone();
            ICurrency pound = new Pound();
            switch (currencyFrom)
            {
                case "euro":
                    euro.balance = amount;
                    break;
                case "krone":
                    krone.balance = amount;
                    break;
                case "pound":
                    pound.balance = amount;
                    break;
            }

            ICurrency convertedToEuro = new Euro { balance = 0 };
            ICurrency convertedToKrone = new Krone { balance = 0 };
            ICurrency convertedToPound = new Pound { balance = 0 };
            switch (currencyTo)
            {
                case "euro":
                    convertedToEuro = new Euro
                    {
                        balance = euro.convertToEuro().balance + krone.convertToEuro().balance +
                                  pound.convertToEuro().balance
                    };
                    ;
                    break;
                case "krone":
                    convertedToKrone = new Krone
                    {
                        balance = euro.convertToKrone().balance + krone.convertToKrone().balance +
                                  pound.convertToKrone().balance
                    };
                    break;
                case "pound":
                    convertedToPound = new Pound
                    {
                        balance = euro.convertToPound().balance + krone.convertToPound().balance +
                                  pound.convertToPound().balance
                    };
                    break;
            }

            UpdateBalanceAsync(accountExchange,
                convertedToEuro.balance + convertedToKrone.balance + convertedToPound.balance, currencyTo);
            scope.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }
    }
    
    public Task<Boolean> DeleteAsync(long id)
    {
        return accountServices.Delete(id);
    }
}
