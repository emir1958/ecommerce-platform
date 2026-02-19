using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
namespace Product.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product.Domain.Entities.Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name).IsRequired();
            entity.Property(p => p.Brand).IsRequired();

            entity.HasMany(p => p.Variants)
                  .WithOne()
                  .HasForeignKey("ProductId")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.SKU)
                  .IsRequired();

            entity.Property(v => v.Price)
                  .HasPrecision(18, 2);   
        });
    }

    public DbSet<Product.Domain.Entities.Product> Products => Set<Product.Domain.Entities.Product>();
}
