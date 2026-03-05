using MediatR;
using Product.Domain.Repositories;
using DomainProduct = Product.Domain.Entities.Product;

namespace Product.Application.Products.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = DomainProduct.Create(
            command.Name,
            command.Description,
             command.Brand
        );

        await _repository.AddAsync(product);

        // SaveChanges TransactionBehavior pipeline'ı tarafından otomatik yapılacak
        // Domain event'ler SaveChanges sırasında Outbox'a yazılacak

        return product.Id;
    }
}
