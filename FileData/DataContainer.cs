using Domain.Model;

namespace FileData;

public class DataContainer
{
    public ICollection<Client> Clients { get; set; }
    public ICollection<Account> Accounts { get; set; }
}