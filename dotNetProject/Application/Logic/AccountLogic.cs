using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class AccountLogic : IAccountLogic
{
    private readonly IAccountDao _accountDao;
    private readonly IClientDao _clientDao;

    public AccountLogic(IAccountDao accountDao, IClientDao clientDao)
    {
        _accountDao = accountDao;
        _clientDao = clientDao;
    }

    public async Task<Account> CreateAsync(AccountCreationDTO dto)
    {
        //This needs updating depending whether the dto has loan true or false
        //Loan true has only one currency and also a value of loan taken, so this needs to be pushed into the currencies entity
        Client? existing = await _clientDao.GetByIdAsync(dto.ownerId);
        if (existing == null)
        {
            throw new Exception($"No client was found with the id {dto.ownerId}!");
        }

        Currency main = new { name = dto.mainCurrency, balance = 0 };
        List<Currency> newCurrencyList = new List<Currency>();
        newCurrencyList.Add(main);
        Account account = new Account { mainCurrency = dto.mainCurrency, loan = dto.loan, ownerId = dto.ownerId,Currencies = newCurrencyList};
        Account created = await _accountDao.CreateAsync(account);
        return created;
    }

    public Task<IEnumerable<Account?>> GetByOwnerIdAsync(long ownerId)
    {
        return _accountDao.GetByOwnerIdAsync(ownerId);
    }

    public Task<Account?> GetByIdAsync(long id)
    {
        if (_accountDao.GetByIdAsync(id) == null)
        {
            throw new Exception("there is no account with that id");
        }
        return _accountDao.GetByIdAsync(id);
    }
}