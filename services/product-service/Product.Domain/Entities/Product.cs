using Product.Domain.Common;
using Product.Domain.Events;
using Product.Domain.Exceptions;

namespace Product.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Brand { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private readonly List<ProductVariant> _variants = new();
    public IReadOnlyCollection<ProductVariant> Variants => _variants;

    protected Product() { }

    private Product(string name, string description,string brand)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Brand = brand;
        IsActive = true;
    }

    public static Product Create(string name, string description,string brand)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name boş olamaz");

        var product = new Product(name, description,brand);

        product.AddDomainEvent(new ProductCreatedEvent(product.Id));

        return product;
    }
    public void Update(string name, string description, string brand)
    {
        Name = name;
        Description = description;
        Brand = brand;
        // Domain event tetikleme mümkün
    }
    public void AddVariant(string sku, decimal price, int stock)
    {
        if (_variants.Any(x => x.Sku == sku))
            throw new DomainException("Bu SKU'ya sahip bir varyant zaten mevcut.");

        _variants.Add(new ProductVariant(sku, price, stock));
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}