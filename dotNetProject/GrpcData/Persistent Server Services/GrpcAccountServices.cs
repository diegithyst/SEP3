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
        Account created = new Account { ownerId = ga.ClientId, loan = ga.Loan, mainCurrency = ga.MainCurrency, name = ga.Name, accountViewId = ga.AccountViewId };
        created.Euro = new Euro();
        created.Krone = new Krone();
        created.Pound = new Pound();

        created.Euro.balance = ga.Euro;
        created.Krone.balance = ga.Krone;
        created.Pound.balance = ga.Pound;
        return Task.FromResult(created);
    }

    public Task<IEnumerable<Account>> GetByOwnerId(long ownerId)
    {
        List<Account> ownerAccounts = new List<Account>();
        PersistentServerClient.GrpcAccounts call = psc.GetClientAccounts(new PersistentServerClient.ClientBasicDTO { ClientId = ownerId });
        foreach (PersistentServerClient.GrpcAccount ga in call.Accounts)
        {
            Account created = new Account { ownerId = ga.ClientId, loan = ga.Loan, mainCurrency = ga.MainCurrency, name = ga.Name, accountViewId = ga.AccountViewId };
            created.Euro = new Euro();
            created.Krone = new Krone();
            created.Pound = new Pound();
            
            created.Euro.balance = ga.Euro;
            created.Krone.balance = ga.Krone;
            created.Pound.balance = ga.Pound;
            ownerAccounts.Add(created);
        }

        return Task.FromResult(ownerAccounts.AsEnumerable());
    }

    public Task<Account> GetById(long id)
    {
        PersistentServerClient.GrpcAccount ga = psc.GetAccountById(new PersistentServerClient.AccountBasicDTO { AccountId = id });
        if (ga != null)
        {
            Account created = new Account { ownerId = ga.ClientId, loan = ga.Loan, mainCurrency = ga.MainCurrency, name = ga.Name, accountViewId = ga.AccountViewId };
            created.Euro = new Euro();
            created.Krone = new Krone();
            created.Pound = new Pound();

            created.Euro.balance = ga.Euro;
            created.Krone.balance = ga.Krone;
            created.Pound.balance = ga.Pound;
            return Task.FromResult(created);
        }
        throw new Exception("There is no account with that id");
    }

    public Task TransferMoney(MoneyTransferCreationDto dto)
    {
        PersistentServerClient.GrpcMoneyTransfer gmt = psc.MakeMoneyTransfer(new PersistentServerClient.CreateMoneyTransferDTO { Sender = dto.SenderAccountNumber, SenderCurrency = dto.Currency, Amount = dto.Value, Commission = dto.Commission, Receipt = dto.Receipt });
        MoneyTransfer created = new MoneyTransfer { Sender = gmt.SenderAccountNumber, SenderCurrency = gmt.Currency, Amount = gmt.Value, Commission = gmt.Commission, Receipt = gmt.Receipt };
        return Task.FromResult(created);
    }
    public Task UpdateAccount(AccountUpdateDTO accountUpdateDTO)
    {
        PersistentServerClient.GrpcAccount ga = psc.UpdateAccount(new PersistentServerClient.AccountUpdateDTO { Name = accountUpdateDTO.name, MainCurrency = accountUpdateDTO.mainCurrency, Euro = accountUpdateDTO.euro, Krone = accountUpdateDTO.krone, Pound = accountUpdateDTO.pound });
        if (ga != null){
            return Task.CompletedTask;
        }
        throw new Exception("Update  failed.");
    }
}