namespace Product.Application.Products.DTOs;

public record ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Brand { get; init; } = default!;
    public bool IsActive { get; init; }
}
