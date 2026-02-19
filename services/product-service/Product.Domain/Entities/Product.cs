using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities;
public class Product : BaseEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<ProductVariant> _variants = new();
    public IReadOnlyCollection<ProductVariant> Variants => _variants;

    protected Product() { }

    public Product(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = true;

        AddDomainEvent(new ProductCreatedEvent(Id));
    }

    public void AddVariant(string sku, decimal price, int stock)
    {
        if (_variants.Any(x => x.Sku == sku))
            throw new Exception("Bu SKU'ya sahip bir varyant zaten mevcut.");

        _variants.Add(new ProductVariant(sku, price, stock));
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}