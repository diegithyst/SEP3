using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class GrpcAccountServices : IGrpcAccountServices
{
    private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;
    public GrpcAccountServices(GrpcContext grpcContext)
    {
        psc = grpcContext.Psc;
    }


    public Task<Account> Create(AccountCreationDTO accountCreationDTO)
    {
         PersistentServerClient.GrpcAccount ga = psc.CreateAccount(new PersistentServerClient.AccountCreationDTO { AccountViewId = accountCreationDTO.accountViewId, ClientId = accountCreationDTO.ownerId, MainCurrency = accountCreationDTO.mainCurrency, Name = accountCreationDTO.name, Loan = accountCreationDTO.loan });
         return Task.FromResult(new Account { ownerId = ga.ClientId, loan = ga.Loan, mainCurrency = ga.MainCurrency, euro = ga.Euro, krone = ga.Krone, pound = ga.Pound, name = ga.Name, accountViewId = ga.AccountViewId });
    }

    public Task<IEnumerable<Account>> GetByOwnerId(long ownerId)
    {
        List<Account> ownerAccounts = new List<Account>();
        PersistentServerClient.GrpcAccounts call = psc.GetClientAccounts(new PersistentServerClient.ClientBasicDTO { ClientId = ownerId });
       foreach(PersistentServerClient.GrpcAccount ga in call.Accounts)
        {
            ownerAccounts.Add(new Account { id = ga.AccountId, ownerId = ga.ClientId, euro = ga.Euro, krone = ga.Krone, pound = ga.Pound, loan = ga.Loan, mainCurrency = ga.MainCurrency });
        }

        return Task.FromResult(ownerAccounts.AsEnumerable());
    }

    public Task<Account> GetById(long id)
    {
     PersistentServerClient.GrpcAccount ga = psc.GetAccountById(new PersistentServerClient.AccountBasicDTO { AccountId = id});
        if (ga != null)
        {
            return Task.FromResult(new Account { id = ga.AccountId, ownerId = ga.ClientId, euro = ga.Euro, krone = ga.Krone, pound = ga.Pound, loan = ga.Loan, mainCurrency = ga.MainCurrency });
        }
        throw new Exception("There is no account with that id");
    }

    public Task TransferMoney(MoneyTransferCreationDto dto)
    {
        throw new NotImplementedException();
    }
}