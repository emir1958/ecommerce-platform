using MediatR;
using Product.Domain.Repositories;
using Product.Application.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Product.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IProductRepository _repository;

        public UpdateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.Id);
            if (product == null)
                throw new KeyNotFoundException($"Product with Id {command.Id} not found.");

            product.Update(
                command.Name,
                command.Description,
                command.Brand
            );

            await _repository.UpdateAsync(product);

            // SaveChanges TransactionBehavior pipeline tarafından yapılacak
            return product.Id;
        }
    }
}
