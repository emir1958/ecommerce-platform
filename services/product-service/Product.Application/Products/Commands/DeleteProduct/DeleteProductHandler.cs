using MediatR;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Application.Products.Commands;


namespace Product.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand,Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                throw new KeyNotFoundException($"Product with Id {request.Id} not found.");

            await _repository.DeleteAsync(product);

            return Unit.Value;
        }
    }
}
