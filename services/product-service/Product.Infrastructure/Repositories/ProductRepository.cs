using Microsoft.EntityFrameworkCore;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Product?> GetByIdAsync(Guid id)
        => await _context.Products
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Domain.Entities.Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    public async Task AddAsync(Domain.Entities.Product product)
        => await _context.Products.AddAsync(product);

    public Task UpdateAsync(Domain.Entities.Product product)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(Domain.Entities.Product product)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }
}