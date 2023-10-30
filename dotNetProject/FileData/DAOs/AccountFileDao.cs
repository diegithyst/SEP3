using Application.DaoInterfaces;
using Domain.Model;

namespace FileData.DAOs;

public class AccountFileDao : IAccountDao
{
    private readonly FileContext _context;
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;
    public AccountFileDao(FileContext context, GrpcContext grpcContext)
    {
        _context = context;
        psc = grpcContext.Psc;
    }


    public Task<Account> CreateAsync(Account account)
    {
        long id = 1;
        if (_context.Accounts.Any())
        {
            id = _context.Accounts.Max(a => a.id);
            id++;
        }

        account.id = id;

        
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return Task.FromResult(account);
    }

    public Task<IEnumerable<Account>> GetByOwnerIdAsync(long ownerId)
    {
        List<Account> ownerAccounts = new List<Account>();
        PersistentServerClient.GrpcAccounts call = psc.GetClientAccounts(new PersistentServerClient.ClientBasicDTO { ClientId = ownerId });
       foreach(PersistentServerClient.GrpcAccount ga in call.Accounts)
        {
            ownerAccounts.Add(new Account { id = ga.AccountId, ownerId = ga.ClientId, balance = ga.Balance, loan = ga.Loan, mainCurrency = ga.MainCurrency });
        }

        return Task.FromResult(ownerAccounts.AsEnumerable());
    }

    public Task<Account> GetByIdAsync(long id)
    {
     PersistentServerClient.GrpcAccount ga = psc.GetAccountById(new PersistentServerClient.AccountBasicDTO { AccountId = id});
        if (ga != null)
        {
            return Task.FromResult(new Account { id = ga.AccountId, ownerId = ga.ClientId, balance = ga.Balance, loan = ga.Loan, mainCurrency = ga.MainCurrency});
        }
        throw new Exception("There is no account with that id");
    }

    public Task TransferMoneyAsync(double amountToTranfer, int accountIdentifier)
    {
        throw new NotImplementedException();
    }
}