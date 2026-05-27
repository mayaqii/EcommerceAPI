using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/orders (Pobierz zamówienia wraz z produktami)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Products) // Eager loading: dołącz produkty z tabeli łączącej
            .ToListAsync();

        var orderDtos = orders.Select(o => new OrderDTO
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            Status = o.Status,
            Products = o.Products.Select(p => new ProductDTO { Id = p.Id, Name = p.Name, Price = p.Price }).ToList()
        });

        return Ok(orderDtos);
    }

    // POST: api/orders (Tworzenie zamówienia z wieloma produktami)
    [HttpPost]
    public async Task<ActionResult<OrderDTO>> CreateOrder(CreateOrderDTO createOrderDto)
    {
        // Znajdź w bazie tylko te produkty, których ID przesłał użytkownik
        var products = await _context.Products
            .Where(p => createOrderDto.ProductIds.Contains(p.Id))
            .ToListAsync();

        if (!products.Any()) return BadRequest("Nie znaleziono podanych produktów.");

        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            Products = products // EF Core sam doda wpisy do tabeli relacji wiele-do-wielu!
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var orderDto = new OrderDTO
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status,
            Products = order.Products.Select(p => new ProductDTO { Id = p.Id, Name = p.Name, Price = p.Price }).ToList()
        };

        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, orderDto);
    }

    // PUT: api/orders/5 (Modyfikacja statusu zamówienia)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        order.Status = status;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/orders/5 (Usuń zamówienie)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}