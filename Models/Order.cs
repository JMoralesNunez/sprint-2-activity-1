namespace Restaurant_Management.Models;

public class Order
{
    public int Id { get; set; }
    
    public int OrderNumber { get; set; }
    
    public DateOnly Date { get; set; }
    
    public string State { get; set; }
    
    public int? fk_ClientID { get; set; }
    
}