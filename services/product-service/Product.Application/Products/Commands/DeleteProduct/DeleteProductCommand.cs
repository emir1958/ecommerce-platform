using MediatR;

namespace Product.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Unit>;