using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class ClientFileDao : IClientDao
{
    private readonly FileContext context;

    public ClientFileDao(FileContext context)
    {
        this.context = context;
    }


    public Task<Client> CreateAsync(Client client)
    {
        context.Clients.Add(client);
        context.SaveChanges();

        return Task.FromResult(client);
    }

    public Task<Client?> GetByIdAsync(string id)
    {
        Client? existing = context.Clients.FirstOrDefault(c => c.identityDocument.Equals(id, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<Client>> GetAsync(SearchClientParametersDto searchParameters)
    {
        IEnumerable<Client> existing = context.Clients.AsEnumerable();
        if (!string.IsNullOrEmpty(searchParameters.id))
        {
            existing  = existing.Where(c => c.identityDocument.Equals(searchParameters.id, StringComparison.OrdinalIgnoreCase));
        }
        return Task.FromResult(existing);
    }
}