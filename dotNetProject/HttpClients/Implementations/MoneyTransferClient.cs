using System.Net.Http.Json;
using Domain.DTOs;
using Domain.Model;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class MoneyTransferClient : IMoneyTransferService
{
    private readonly HttpClient client;

    public MoneyTransferClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(MoneyTransferCreationDto dto)
    {
        HttpResponseMessage message = await client.PostAsJsonAsync("/transfer", dto);
        string content = await message.Content.ReadAsStringAsync();
        if (!message.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public Task<ICollection<MoneyTransfer>> GetListByAccountIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<MoneyTransfer> GetByIdAsync()
    {
        throw new NotImplementedException();
    }
}