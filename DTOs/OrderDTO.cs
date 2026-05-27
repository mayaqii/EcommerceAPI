namespace EcommerceAPI.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<ProductDTO> Products { get; set; } = new();
}