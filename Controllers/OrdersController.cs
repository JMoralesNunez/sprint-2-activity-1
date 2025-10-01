using Microsoft.AspNetCore.Mvc;
using Restaurant_Management.Data;
using Restaurant_Management.Models;
using Restaurant_Management.ViewModels;
namespace Restaurant_Management.Controllers;

public class OrdersController : Controller
{
    private readonly PostgresContext _context;
    
    public OrdersController(PostgresContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        // ViewModel
        var viewModel = new OrdersViewModel
        {
            Clients = _context.Clients.ToList(),
            Orders = _context.Orders.ToList()
        };

        return View(viewModel);
    }
    
    public IActionResult Details(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
    
    public IActionResult Store([Bind("OrderNumber,Date,State,ClientId")] Order order)
    {

        if (ModelState.IsValid)
        {
            _context.Add(order);
            _context.SaveChanges();
            TempData["message"] = "Pedido agregado con éxito";
            return RedirectToAction(nameof(Index));
        }

        return BadRequest();
    }
    
    public IActionResult Edit(int id, [Bind("Id,OrderNumber,Date,State,ClientId")] Order NewOrder)
    {
        
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return NotFound();
        }
    
        if (ModelState.IsValid)
        {
            order.OrderNumber = NewOrder.OrderNumber;
            order.Date = NewOrder.Date;
            order.State = NewOrder.State;
            order.fk_ClientID = NewOrder.fk_ClientID;
            _context.SaveChanges();
            TempData["message"] = "La orden ha sido editada";
        }
        return RedirectToAction(nameof(Index));
    }       

    

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }
    
    public IActionResult Destroy(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        _context.SaveChanges();
        TempData["message"] = "La orden ha sido eliminada";
        return RedirectToAction(nameof(Index));
    }
}
