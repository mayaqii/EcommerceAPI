using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;

namespace EcommerceAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Definiujemy, jakie tabele mają powstać w bazie danych
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}