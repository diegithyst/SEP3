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

    public async Task<IEnumerable<MoneyTransfer?>?> GetBySearchAsync(long? receiverAccount, long? senderAccount)
    {
        string query = ConstructQuery(receiverAccount, senderAccount);

        HttpResponseMessage response = await client.GetAsync("/trasnfers" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IEnumerable<MoneyTransfer> transfers = JsonSerializer.Deserialize<IEnumerable<MoneyTransfer>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return transfers;
    }

    public async Task<IEnumerable<MoneyTransfer?>?> GetAsync()
    {
        HttpResponseMessage response = await client.GetAsync($"/transfer");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IEnumerable<MoneyTransfer?>? transfers = JsonSerializer.Deserialize<IEnumerable<MoneyTransfer>>(content);
        return transfers;
    }

    public async Task<MoneyTransfer?> GetByIdAsync(long id)
    {
        HttpResponseMessage response = await client.GetAsync($"/transfers/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        MoneyTransfer transfer = JsonSerializer.Deserialize<MoneyTransfer>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return transfer;
    }

    private static string ConstructQuery(long? receiverAccount, long? senderAccount)
    {
        string query = "";
        if (receiverAccount != null)
        {
            query += $"accountNumberRecipient={receiverAccount}";
        }

        if (senderAccount != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"accountNumberSender={senderAccount}";
        }
        
        return query;
    }
}