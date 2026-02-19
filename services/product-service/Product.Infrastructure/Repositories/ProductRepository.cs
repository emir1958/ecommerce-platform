using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
namespace Product.Infrastructure.Repositories;

public class ProductRepository : Product.Domain.Repositories.IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product.Domain.Entities.Product?> GetByIdAsync(Guid id)
        => await _context.Products
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Product.Domain.Entities.Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    public async Task AddAsync(Product.Domain.Entities.Product product)
        => await _context.Products.AddAsync(product);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}