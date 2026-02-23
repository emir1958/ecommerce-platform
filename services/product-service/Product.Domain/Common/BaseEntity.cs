namespace Product.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    private readonly List<BaseDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(BaseDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}