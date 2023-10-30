using Application.DaoInterfaces;
using Domain.Model;

namespace FileData.DAOs;

public class AccountFileDao : IAccountDao
{
    private readonly FileContext _context;

    public AccountFileDao(FileContext context)
    {
        _context = context;
    }


    public Task<Account> CreateAsync(Account account)
    {
        int id = 1;
        if (_context.Accounts.Any())
        {
            id = _context.Accounts.Max(a => a.identifier);
            id++;
        }

        account.identifier = id;

        
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return Task.FromResult(account);
    }

    public Task<IEnumerable<Account>> GetByOwnerIdAsync(string ownerId)
    {
        IEnumerable<Account> accounts = _context.Accounts.AsEnumerable();

        if (!string.IsNullOrEmpty(ownerId))
        {
            accounts = _context.Accounts.Where(a => a.ownerId.Equals(ownerId, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(accounts);
    }

    public Task<Account> GetByIdAsync(int id)
    {
        Account? existing = _context.Accounts.FirstOrDefault(a => a.identifier == id);
        if (existing != null)
        {
            return Task.FromResult(existing);
        }
        throw new Exception("There is no account with that id");
    }

    public Task TransferMoneyAsync(double amountToTranfer, int accountIdentifier)
    {
        throw new NotImplementedException();
    }
}