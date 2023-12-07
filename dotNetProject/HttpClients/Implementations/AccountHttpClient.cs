using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Model;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class AccountHttpClient : IAccountService
{

    private readonly HttpClient client;

    public AccountHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Account> GetAccountAsync(long id)
    {
        string uri = "Accounts/getAccountById";
        if (id != 0)
        {
            uri += $"?accountId={id}";
        }

        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        AccountJsonDto accountDto = JsonSerializer.Deserialize<AccountJsonDto>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Account newAccount = new Account();
        newAccount.id = accountDto.id;
        newAccount.name = accountDto.name;
        newAccount.mainCurrency = accountDto.mainCurrency;
        newAccount.Euro = accountDto.euro;
        newAccount.Krone = accountDto.krone;
        newAccount.Pound = accountDto.pound;
        newAccount.ownerId = accountDto.ownerId;
        newAccount.loan = accountDto.loan;


        return newAccount;
    }

    public async Task CreateAsync(AccountCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/accounts", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Account>> GetByClientIdAsync(long id)
    {
        HttpResponseMessage response = await client.GetAsync($"/accounts/?ownerId={id}");
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        ICollection<AccountJsonDto> accounts = JsonSerializer.Deserialize<ICollection<AccountJsonDto>>(content);
        ICollection<Account> accountList = new List<Account>();
        foreach (var accountDto in accounts)
        {
            Account newAccount = new Account();
            newAccount.id = accountDto.id;
            newAccount.name = accountDto.name;
            newAccount.mainCurrency = accountDto.mainCurrency;
            newAccount.Euro = accountDto.euro;
            newAccount.Krone = accountDto.krone;
            newAccount.Pound = accountDto.pound;
            newAccount.ownerId = accountDto.ownerId;
            newAccount.loan = accountDto.loan;
            accountList.Add(newAccount);
        }
        return accountList;
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }
}