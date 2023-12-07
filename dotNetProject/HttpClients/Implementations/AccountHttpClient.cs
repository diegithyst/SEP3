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

        Account account = JsonSerializer.Deserialize<Account>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return account;
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

    public async Task<ICollection<Account>> GetAccountsByClientIdAsync(long id)
    {
        HttpResponseMessage response = await client.GetAsync($"/Accounts/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        ICollection<AccountUpdateDTO> accounts = JsonSerializer.Deserialize<ICollection<AccountUpdateDTO>>(content);
        ICollection<Account> accountList = new List<Account>();
        foreach (var accountDto in accounts)
        {
            Account newAccount = new Account();
            newAccount.id = accountDto.id;
            newAccount.name = accountDto.name;
            newAccount.mainCurrency = accountDto.mainCurrency;
            newAccount.Euro.balance = accountDto.euro;
            newAccount.Krone.balance = accountDto.krone;
            newAccount.Pound.balance = accountDto.pound;
            newAccount.ownerId = accountDto.clientId;
            newAccount.loan = false;
            accountList.Add(newAccount);
        }
        return accountList;
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }
}