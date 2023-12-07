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

    public async Task<ICollection<Account>> GetByClientIdAsync(long id)
    {
        HttpResponseMessage response = await client.GetAsync($"/accounts/?ownerId={id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Account>? accounts = JsonSerializer.Deserialize<ICollection<Account>>(content);
        return accounts;
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }
}