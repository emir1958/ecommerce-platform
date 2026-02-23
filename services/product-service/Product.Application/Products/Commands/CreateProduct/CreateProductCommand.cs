using MediatR;

namespace Product.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    string Brand
) : IRequest<Guid>;
