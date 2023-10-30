using System.Text.Json;
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
    
    
    public async Task<Account> GetAccount(long id)
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
}