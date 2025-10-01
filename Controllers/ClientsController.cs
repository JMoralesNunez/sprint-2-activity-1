using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Data;
using Restaurant_Management.Models;

namespace Restaurant_Management.Controllers;

public class ClientsController : Controller
{
    private readonly PostgresContext _context;
    
    public ClientsController(PostgresContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        // ViewModel
        var clients = _context.Clients.ToList().OrderBy(c => c.Id); 
        return View(clients);
    }
    
    public IActionResult Details(int id)
    {
        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }
    
    public IActionResult Store([Bind("Name,LastNames,Email,Phone")] Client client)
    {

        if (ModelState.IsValid)
        {
            _context.Add(client);
            _context.SaveChanges();
            TempData["message"] = "Cliente agregado con éxito";
            return RedirectToAction(nameof(Index));
        }

        return BadRequest();
    }
    
    public IActionResult Edit(int id, [Bind("Id,Name,LastNames,Email,Phone")] Client newclient)
    {
        
        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }
    
        if (ModelState.IsValid)
        {
            client.Name = newclient.Name;
            client.LastNames = newclient.LastNames;
            client.Email = newclient.Email;
            client.Phone = newclient.Phone;
            _context.SaveChanges();
            TempData["message"] = "El usuario ha sido editado";
        }
        return RedirectToAction(nameof(Index));
    }       

    

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }
        return View(client);
    }
    
    public IActionResult Destroy(int id)
    {
        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(client);
        _context.SaveChanges();
        TempData["message"] = "El usuario ha sido eliminado";
        return RedirectToAction(nameof(Index));
    }
}