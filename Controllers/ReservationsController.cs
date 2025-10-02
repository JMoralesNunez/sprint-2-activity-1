using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Data;
using Restaurant_Management.Models;
using Restaurant_Management.ViewModels;
namespace Restaurant_Management.Controllers;

public class ReservationsController : Controller
{
    private readonly PostgresContext _context;
    
    public ReservationsController(PostgresContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        // ViewModel
        var viewModel = new ReservationsViewModel
        {
            Clients = _context.Clients.ToList(),
            Reservations = _context.Reservations.ToList()
        };

        return View(viewModel);
    }
    
    public IActionResult Details(int id)
    {
        var reservation = _context.Reservations.Find(id);
        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }
    
    public IActionResult Store([Bind("Date,Hour,NumberPeople,Observations,fk_ClientId")] Reservation reservation)
    {

        if (ModelState.IsValid)
        {
            _context.Add(reservation);
            _context.SaveChanges();
            TempData["message"] = "Reserva agregada con éxito";
            return RedirectToAction(nameof(Index));
        }

        return BadRequest();
    }
    
    public IActionResult Edit(int id, [Bind("Id,Date,Hour,NumberPeople,Observations,fk_ClientId")] Reservation NewReservation)
    {
        var reservation = _context.Reservations.Find(id);
        if (reservation == null)
        {
            return NotFound();
        }
    
        if (ModelState.IsValid)
        {   
            reservation.Date = NewReservation.Date;
            reservation.Hour = NewReservation.Hour;
            reservation.NumberPeople = NewReservation.NumberPeople;
            reservation.Observations = NewReservation.Observations;
            reservation.fk_ClientId = NewReservation.fk_ClientId;
            _context.SaveChanges();
            TempData["message"] = "La reserva ha sido editada";
        }
        return RedirectToAction(nameof(Index));
    }       

    

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewBag.Clients = _context.Clients.ToList();
        
        var reservation = _context.Reservations.Find(id);
        if (reservation == null)
        {
            return NotFound();
        }
        return View(reservation);
    }
    
    public IActionResult Destroy(int id)
    {
        var reservation = _context.Reservations.Find(id);
        if (reservation == null)
        {
            return NotFound();
        }

        _context.Reservations.Remove(reservation);
        _context.SaveChanges();
        TempData["message"] = "La reserva ha sido eliminada";
        return RedirectToAction(nameof(Index));
    }
}