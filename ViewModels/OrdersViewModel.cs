using Restaurant_Management.Models;
namespace Restaurant_Management.ViewModels;

public class OrdersViewModel
{
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Order> Orders { get; set; }
}