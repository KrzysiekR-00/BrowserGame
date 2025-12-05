// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<ProductDto> Get() => new[]
    {
        new ProductDto(1, "Laptop"),
        new ProductDto(2, "Monitor")
    };
}

public record ProductDto(int Id, string Name);

