using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Data;
using Restaurant_Management.Models;

namespace Restaurant_Management.Controllers;

public class PlatesController : Controller
{
    private readonly PostgresContext _context;
    
    public PlatesController(PostgresContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        // ViewModel
        var plates = _context.Plates.ToList().OrderBy(c => c.Id); 
        return View(plates);
    }
    
    public IActionResult Details(int id)
    {
        var plates = _context.Plates.Find(id);
        if (plates == null)
        {
            return NotFound();
        }

        return View(plates);
    }
    
    public IActionResult Store([Bind("Name,Description,Price,Category")] Plate plate)
    {

        if (ModelState.IsValid)
        {
            _context.Add(plate);
            _context.SaveChanges();
            TempData["message"] = "Plato agregado con éxito";
            return RedirectToAction(nameof(Index));
        }

        return BadRequest();
    }
    
    public IActionResult Edit(int id, [Bind("Id,Name,Description,Price,Category")] Plate newPlate)
    {
        
        var plate = _context.Plates.Find(id);
        if (plate == null)
        {
            return NotFound();
        }
    
        if (ModelState.IsValid)
        {
            plate.Name = newPlate.Name;
            plate.Description = newPlate.Description;
            plate.Price = newPlate.Price;
            plate.Category = newPlate.Category;
            _context.SaveChanges();
            TempData["message"] = "El plato ha sido editado";
        }
        return RedirectToAction(nameof(Index));
    }       

    

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var plate = _context.Plates.Find(id);
        if (plate == null)
        {
            return NotFound();
        }
        return View(plate);
    }
    
    public IActionResult Destroy(int id)
    {
        var plate = _context.Plates.Find(id);
        if (plate == null)
        {
            return NotFound();
        }

        _context.Plates.Remove(plate);
        _context.SaveChanges();
        TempData["message"] = "El plato ha sido eliminado";
        return RedirectToAction(nameof(Index));
    }
}