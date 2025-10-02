using Restaurant_Management.Models;
namespace Restaurant_Management.ViewModels;

public class ReservationsViewModel
{
    public IEnumerable<Client> Clients { get; set; }
    public IEnumerable<Reservation> Reservations { get; set; }
}