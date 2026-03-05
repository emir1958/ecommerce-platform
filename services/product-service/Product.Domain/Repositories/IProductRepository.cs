namespace Product.Domain.Repositories;

public interface IProductRepository
{
    Task<Entities.Product?> GetByIdAsync(Guid id);

    Task<List<Entities.Product>> GetAllAsync();

    Task AddAsync(Entities.Product product);
    Task UpdateAsync(Entities.Product product);
    Task DeleteAsync(Entities.Product product);
}