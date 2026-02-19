using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Domain.Entities;

namespace Product.Domain.Repositories;

public interface IProductRepository
{
    Task<Entities.Product?> GetByIdAsync(Guid id);

    Task<List<Entities.Product>> GetAllAsync();

    Task AddAsync(Entities.Product product);

    Task SaveChangesAsync();
}