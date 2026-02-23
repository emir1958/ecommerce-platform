using Product.Domain.Exceptions;

namespace Product.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; private set; }
    public string Sku { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    protected ProductVariant() { }

    public ProductVariant(string sku, decimal price, int stock)
    {
        Id = Guid.NewGuid();
        Sku = sku;
        Price = price;
        Stock = stock;
    }

    public void IncreaseStock(int quantity)
    {
        Stock += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (Stock < quantity)
            throw new DomainException("Yetersiz stok");

        Stock -= quantity;
    }
}
