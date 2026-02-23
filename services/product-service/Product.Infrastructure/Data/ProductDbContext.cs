using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Product.Application.Common.Interfaces;
using Product.Domain.Common;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence.Outbox;

namespace Product.Infrastructure.Data;

public class ProductDbContext : DbContext, IUnitOfWork
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.Entities.Product> Products => Set<Domain.Entities.Product>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasMaxLength(2000);
            entity.Property(p => p.Brand).HasMaxLength(100);

            entity.HasMany(p => p.Variants)
                  .WithOne()
                  .HasForeignKey("ProductId")
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Ignore(p => p.DomainEvents);
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Sku)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(v => v.Price)
                  .HasPrecision(18, 2);
        });

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.Type)
                  .IsRequired()
                  .HasMaxLength(500);

            entity.Property(o => o.Payload)
                  .IsRequired();

            entity.HasIndex(o => o.ProcessedOn)
                  .HasFilter("[ProcessedOn] IS NULL")
                  .HasDatabaseName("IX_OutboxMessages_Unprocessed");
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Domain event'leri Outbox tablosuna yaz
        ConvertDomainEventsToOutboxMessages();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDomainEventsToOutboxMessages()
    {
        var entitiesWithEvents = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                var outboxMessage = OutboxMessage.Create(
                    domainEvent.GetType().FullName!,
                    JsonSerializer.Serialize(domainEvent, domainEvent.GetType())
                );

                OutboxMessages.Add(outboxMessage);
            }

            entity.ClearDomainEvents();
        }
    }
}
