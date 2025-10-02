namespace Restaurant_Management.Models;

public class Reservation
{
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    public TimeOnly Hour { get; set; }
    
    public int NumberPeople { get; set; }
    
    public string Observations { get; set; }
    
    public int fk_ClientId { get; set; }
}