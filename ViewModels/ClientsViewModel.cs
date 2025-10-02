using Restaurant_Management.Models;

namespace Restaurant_Management.ViewModels;

public class ClientsViewModel
{
    public Client NewClient { get; set; }
    public IEnumerable<Client> Clients { get; set; }
}