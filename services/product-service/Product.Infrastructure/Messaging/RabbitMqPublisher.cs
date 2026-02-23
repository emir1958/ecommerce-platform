using Product.Application.Common.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Product.Infrastructure.Messaging;

public class RabbitMqPublisher : IEventPublisher
{
    private readonly IConnection _connection;

    public RabbitMqPublisher(IConnection connection)
    {
        _connection = connection;
    }

    public void Publish(string queueName, object message)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }
}