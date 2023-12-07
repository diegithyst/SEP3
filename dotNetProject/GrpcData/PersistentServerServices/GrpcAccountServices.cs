using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;
using FileData;

namespace Grpc.PersistentSeverServices;

public class GrpcAccountServices : IGrpcAccountServices
{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;
    public GrpcAccountServices(GrpcContext grpcContext)
    {
        psc = grpcContext.Psc;
    }


    public Task<Account> Create(Account account)
    {
        PersistentServerClient.GrpcAccount ga = psc.CreateAccount(new PersistentServerClient.AccountCreationDTO { ClientId = account.ownerId, MainCurrency = account.mainCurrency, Name = account.name, Loan = account.loan, Euro = account.Euro.balance, Krone = account.Krone.balance, Pound = account.Pound.balance });
        Account created = new Account { id = ga.AccountId, ownerId = ga.ClientId, loan = ga.Loan, mainCurrency = ga.MainCurrency, name = ga.Name };
        created.Euro = new Euro();
        created.Krone = new Krone();
        created.Pound = new Pound();

        created.Euro.balance = ga.Euro;
        created.Krone.balance = ga.Krone;
        created.Pound.balance = ga.Pound;
        created.Euro.accountId = ga.AccountId;
        created.Krone.accountId = ga.AccountId;
        created.Pound.accountId = ga.AccountId;
        return Task.FromResult(created);
    }

    public Task<IEnumerable<Account>> GetByOwnerId(long ownerId)
    {
        List<Account> ownerAccounts = new List<Account>();
        PersistentServerClient.GrpcAccounts call = psc.GetClientAccounts(new PersistentServerClient.ClientBasicDTO { ClientId = ownerId });
        foreach (PersistentServerClient.GrpcAccount ga in call.Accounts)
        {
            Account created = new Account { id = ga.AccountId, ownerId = ga.ClientId, loan = ga.Loan, mainCurrency = ga.MainCurrency, name = ga.Name };
            created.Euro = new Euro();
            created.Krone = new Krone();
            created.Pound = new Pound();
            
            created.Euro.balance = ga.Euro;
            created.Krone.balance = ga.Krone;
            created.Pound.balance = ga.Pound;
            created.Euro.accountId = ga.AccountId;
            created.Krone.accountId = ga.AccountId;
            created.Pound.accountId = ga.AccountId;
            ownerAccounts.Add(created);
        }

        return Task.FromResult(ownerAccounts.AsEnumerable());
    }

    public Task<Account> GetById(long id)
    {
        PersistentServerClient.GrpcAccount ga = psc.GetAccountById(new PersistentServerClient.AccountBasicDTO { AccountId = id });
        if (ga != null)
        {
            Account created = new Account { id = ga.AccountId, ownerId = ga.ClientId, loan = ga.Loan, mainCurrency = ga.MainCurrency, name = ga.Name };
            created.Euro = new Euro();
            created.Krone = new Krone();
            created.Pound = new Pound();

            created.Euro.balance = ga.Euro;
            created.Krone.balance = ga.Krone;
            created.Pound.balance = ga.Pound;
            created.Euro.accountId = ga.AccountId;
            created.Krone.accountId = ga.AccountId;
            created.Pound.accountId = ga.AccountId;
            return Task.FromResult(created);
        }
        throw new Exception("There is no account with that id");
    }

  
    public Task UpdateAccount(AccountUpdateDTO accountUpdateDTO)
    {
        PersistentServerClient.GrpcAccount ga = psc.UpdateAccount(new PersistentServerClient.AccountUpdateDTO { AccountId = accountUpdateDTO.id, ClientId = accountUpdateDTO.clientId , Name = accountUpdateDTO.name, MainCurrency = accountUpdateDTO.mainCurrency, Euro = accountUpdateDTO.euro, Krone = accountUpdateDTO.krone, Pound = accountUpdateDTO.pound });
        if (ga != null){
            return Task.CompletedTask;
        }
        throw new Exception("Update  failed.");
    }
    public Task<Boolean> Delete(long id)
    {
        PersistentServerClient.GrpcResult result = psc.DeleteAccount(new PersistentServerClient.AccountBasicDTO { AccountId = id });
        return Task.FromResult(result.Success);
    }
}