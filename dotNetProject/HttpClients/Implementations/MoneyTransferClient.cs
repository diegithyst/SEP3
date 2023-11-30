using System.Net.Http.Json;
using System.Text.Json;
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

    public async Task<ICollection<MoneyTransfer>> GetListByAccountIdAsync(long id)
    {
        HttpResponseMessage message = await client.GetAsync($"/TransferMoney/{id}");
        string content = await message.Content.ReadAsStringAsync();
        if (!message.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<MoneyTransfer> moneyTransfers = JsonSerializer.Deserialize<ICollection<MoneyTransfer>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        return moneyTransfers;
    }
    
}