using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Products.Commands.CreateProduct;
using Product.Application.Products.Queries.GetAllProducts;

namespace Product.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await _sender.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id }, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _sender.Send(new GetAllProductsQuery());
        return Ok(products);
    }
}
