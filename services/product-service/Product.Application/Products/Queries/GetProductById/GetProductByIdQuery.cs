using MediatR;
using Product.Application.Products.DTOs;

namespace Product.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;