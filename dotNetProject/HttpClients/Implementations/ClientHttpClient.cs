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
    public async Task<ICollection<ClientUpdateDTO>> GetAsync()
    {
        HttpResponseMessage responseMessage = await client.GetAsync("/client");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<ClientUpdateDTO> clients = JsonSerializer.Deserialize<ICollection<ClientUpdateDTO>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;;
        return clients;
    }
    
    public async Task CreateAsync(ClientCreationDTO dto)
    {
        string userAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync("https://localhost:7112/Auth/register", content);
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception();
        }
    }

    public async Task<ClientUpdateDTO> GetByIdAsync(long id)
    {
        HttpResponseMessage responseMessage = await client.GetAsync($"/clients/{id}");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ClientUpdateDTO tmp = JsonSerializer.Deserialize<ClientUpdateDTO>(content, new JsonSerializerOptions
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

    public async Task UpdateAsync(ClientUpdateDTO updateDto)
    {
        string dtoAsJson = JsonSerializer.Serialize(updateDto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/clients", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task RegisterAsync(ClientCreationDTO dto)
    {
        string userAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync("https://localhost:7112/Auth/register", content);
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception();
        }
    }
}