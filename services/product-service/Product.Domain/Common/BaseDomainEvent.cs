using MediatR;

namespace Product.Domain.Common;

public abstract class BaseDomainEvent : INotification
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
}
