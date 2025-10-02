using System.ComponentModel.DataAnnotations;

namespace Restaurant_Management.Models;

public class Client
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    public string LastNames { get; set; }
    
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "El Teléfono es obligatorio")]
    public string Phone { get; set; }
}