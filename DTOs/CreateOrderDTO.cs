namespace EcommerceAPI.DTOs;

public class CreateOrderDTO
{
    // Klient przesyła tylko listę ID produktów, które chce zamówić
    public List<int> ProductIds { get; set; } = new();
}