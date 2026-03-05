using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Products.Commands.CreateProduct;
using Product.Application.Products.Commands.UpdateProduct;
using Product.Application.Products.Commands.DeleteProduct;
using Product.Application.Products.Queries.GetAllProducts;
using Product.Application.Products.Queries.GetProductById;

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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _sender.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _sender.Send(command);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _sender.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}
