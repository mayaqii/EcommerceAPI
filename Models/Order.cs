namespace EcommerceAPI.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";
    public List<Product> Products { get; set; } = new();
}