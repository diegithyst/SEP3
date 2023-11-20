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
        Client? existing = await _clientDao.GetByIdAsync(dto.ownerId);
        if (existing == null)
        {
            throw new Exception($"No client was found with the id {dto.ownerId}!");
        }

        ICurrency main = new ICurrency { name = dto.mainCurrency, balance = 0 };
        List<ICurrency> newCurrencyList = new List<ICurrency>();
        newCurrencyList.Add(main);
        Account account = new Account { mainCurrency = dto.mainCurrency, loan = dto.loan, ownerId = dto.ownerId,Currencies = new CurrencyList()};
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