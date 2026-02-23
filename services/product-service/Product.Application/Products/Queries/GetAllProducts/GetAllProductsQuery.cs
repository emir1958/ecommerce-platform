using MediatR;
using Product.Application.Products.DTOs;

namespace Product.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<ProductDto>>;
