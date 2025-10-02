using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Data;
using Restaurant_Management.Models;
using Restaurant_Management.ViewModels;

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
        var vm = new ClientsViewModel
        {
            NewClient = new Client(),
            Clients = _context.Clients.ToList().OrderBy(c => c.Id)
        };
        return View(vm);
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
    
    public IActionResult Store(ClientsViewModel client)
    {

        if (!ModelState.IsValid)
        {
            client.Clients = _context.Clients.ToList().OrderBy(c => c.Id);
            return View("Index", client);
        }
        _context.Add(client.NewClient);
        _context.SaveChanges();
        TempData["message"] = "Cliente agregado con éxito";
        return RedirectToAction(nameof(Index));
        
    }
    
    /*public IActionResult Edit(int id, /*[Bind("Id,Name,LastNames,Email,Phone")]#1# Client newclient)
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
    }      */ 

    public IActionResult Edit(Client newclient)
    {
        if (!ModelState.IsValid)
        {
            // Volvemos a la vista con los errores y los datos introducidos
            return View(newclient);
        }

        var client = _context.Clients.Find(newclient.Id);
        if (client == null)
        {
            return NotFound();
        }

        client.Name = newclient.Name;
        client.LastNames = newclient.LastNames;
        client.Email = newclient.Email;
        client.Phone = newclient.Phone;

        _context.SaveChanges();
        TempData["message"] = "El usuario ha sido editado";
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