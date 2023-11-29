using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Model;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class ClientHttpClient : IClientService
{
    private readonly HttpClient client;
    
    public ClientHttpClient(HttpClient client)
    {
        this.client = client;
    } 
    public async Task<ICollection<Client>> GetAsync()
    {
        HttpResponseMessage responseMessage = await client.GetAsync("/client");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Client> clients = JsonSerializer.Deserialize<ICollection<Client>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;;
        return clients;
    }

    public async Task<Client> GetByIdAsync(long id)
    {
        HttpResponseMessage responseMessage = await client.GetAsync($"/client/{id}");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Client tmp = JsonSerializer.Deserialize<Client>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return tmp;
    }

    public async Task DeleteAsync(long id)
    {
        HttpResponseMessage message = await client.DeleteAsync($"/clients/{id}");
        string content = await message.Content.ReadAsStringAsync();
        if (!message.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }

    public async Task RegisterAsync(ClientCreationDTO dto)
    {
        string userAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync("https://localhost:7017/Auth/register", content);
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception();
        }
    }
}