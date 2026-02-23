using Product.Domain.Common;

namespace Product.Domain.Events;

public class ProductCreatedEvent : BaseDomainEvent
{
    public Guid ProductId { get; }

    public ProductCreatedEvent(Guid productId)
    {
        ProductId = productId;
    }
}
