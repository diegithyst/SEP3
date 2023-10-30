using System.Text.Json;
using Domain.Model;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<Account> Accounts
    {
        get
        {
            LoadData();
            return _dataContainer.Accounts;
        }
    }

    public ICollection<Client> Clients
    {
        get
        {
            LoadData();
            return _dataContainer.Clients;
        }
    }

    private void LoadData()
    {
        if (_dataContainer != null) 
        {
            return;
        }

        if (!File.Exists(filePath))
        {
            _dataContainer = new()
            {
                Clients = new List<Client>(),
                Accounts = new List<Account>()
            };
                return;
        }

        string content = File.ReadAllText(filePath);
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_dataContainer);
        File.WriteAllText(filePath, serialized);
        _dataContainer = null;
    }
}