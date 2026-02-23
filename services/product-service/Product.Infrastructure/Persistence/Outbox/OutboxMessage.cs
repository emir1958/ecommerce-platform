namespace Product.Infrastructure.Persistence.Outbox;

public class OutboxMessage
{
    public Guid Id { get; private set; }
    public DateTime OccurredOn { get; private set; }
    public string Type { get; private set; } = default!;
    public string Payload { get; private set; } = default!;
    public DateTime? ProcessedOn { get; private set; }

    private OutboxMessage() { }

    public static OutboxMessage Create(string type, string payload)
    {
        return new OutboxMessage
        {
            Id = Guid.NewGuid(),
            OccurredOn = DateTime.UtcNow,
            Type = type,
            Payload = payload,
            ProcessedOn = null
        };
    }

    public void MarkAsProcessed()
    {
        ProcessedOn = DateTime.UtcNow;
    }
}
