namespace Product.Application.Common.Interfaces;

public interface IEventPublisher
{
    void Publish(string queueName, object message);
}
