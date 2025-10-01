using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Data;
using Restaurant_Management.Models;
namespace Restaurant_Management.Controllers;

public class WaitersController :Controller
{
    private readonly PostgresContext _context;
    
    public WaitersController(PostgresContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        // ViewModel
        var waiters = _context.Waiters.ToList().OrderBy(c => c.Id); 
        return View(waiters);
    }
    
    public IActionResult Details(int id)
    {
        var waiter = _context.Waiters.Find(id);
        if (waiter == null)
        {
            return NotFound();
        }

        return View(waiter);
    }
    
    public IActionResult Store([Bind("Name,LastNames,Shift,Experience")] Waiter waiter)
    {

        if (ModelState.IsValid)
        {
            _context.Add(waiter);
            _context.SaveChanges();
            TempData["message"] = "Mesero/a agregado con éxito";
            return RedirectToAction(nameof(Index));
        }

        return BadRequest();
    }
    
    public IActionResult Edit(int id, [Bind("Id,Name,LastNames,Shift,Experience")] Waiter newWaiter)
    {
        
        var waiter = _context.Waiters.Find(id);
        if (waiter == null)
        {
            return NotFound();
        }
    
        if (ModelState.IsValid)
        {
            waiter.Name = newWaiter.Name;
            waiter.LastNames = newWaiter.LastNames;
            waiter.Shift = newWaiter.Shift;
            waiter.Experience = newWaiter.Experience;
            _context.SaveChanges();
            TempData["message"] = "El mesero/a ha sido editado";
        }
        return RedirectToAction(nameof(Index));
    }       

    

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var waiter = _context.Waiters.Find(id);
        if (waiter == null)
        {
            return NotFound();
        }
        return View(waiter);
    }
    
    public IActionResult Destroy(int id)
    {
        var waiter = _context.Waiters.Find(id);
        if (waiter == null)
        {
            return NotFound();
        }

        _context.Waiters.Remove(waiter);
        _context.SaveChanges();
        TempData["message"] = "El mesero/a ha sido eliminado/a";
        return RedirectToAction(nameof(Index));
    }
}