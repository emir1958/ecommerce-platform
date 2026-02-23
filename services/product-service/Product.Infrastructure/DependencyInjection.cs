using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Common.Interfaces;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Messaging;
using Product.Infrastructure.Repositories;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
namespace Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // EF Core + PostgreSQL
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("PostgreSql")
            )
        );

        // UnitOfWork
        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ProductDbContext>());

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        // RabbitMQ
        services.AddSingleton<IConnection>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMQ:HostName") ?? "localhost",
                UserName = configuration.GetValue<string>("RabbitMQ:UserName") ?? "guest",
                Password = configuration.GetValue<string>("RabbitMQ:Password") ?? "guest"
            };

            return factory.CreateConnection();
        });

        services.AddSingleton<IEventPublisher, RabbitMqPublisher>();

        return services;
    }
}
