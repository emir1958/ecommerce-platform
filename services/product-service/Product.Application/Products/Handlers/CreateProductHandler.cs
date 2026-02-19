using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Products.Handlers;

public class CreateProductHandler
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(Commands.CreateProductCommand command)
    {
        var product = new Product.Domain.Entities.Product(
            command.Name,
            command.Description,
            command.Brand
        );

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        return product.Id;
    }
}
