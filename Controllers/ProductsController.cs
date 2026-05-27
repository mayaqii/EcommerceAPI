using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products (Pobierz wszystkie)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products.Select(p => new ProductDTO { Id = p.Id, Name = p.Name, Price = p.Price }));
    }

    // GET: api/products/5 (Pobierz jeden)
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        return Ok(new ProductDTO { Id = product.Id, Name = product.Name, Price = product.Price });
    }

    // POST: api/products (Dodaj nowy)
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDto)
    {
        var product = new Product { Name = productDto.Name, Price = productDto.Price };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        productDto.Id = product.Id;
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
    }

    // PUT: api/products/5 (Edytuj)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDto)
    {
        if (id != productDto.Id) return BadRequest();

        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        product.Name = productDto.Name;
        product.Price = productDto.Price;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/products/5 (Usuń)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}