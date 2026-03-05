using MediatR;

namespace Product.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    string Brand
) : IRequest<Guid>;
