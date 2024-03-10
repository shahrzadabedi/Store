using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Configurations;

namespace Store.Infrastructure;

public class DataContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());

        builder.Entity<Product>().HasIndex(p => p.Title)
            .IsUnique();

        builder.Entity<Product>().Property(p => p.Title)
            .HasMaxLength(40);

        builder.Entity<Order>().HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId);

        builder.Entity<Order>().HasOne(o => o.Buyer)
            .WithMany(b => b.Orders)
            .HasForeignKey(o => o.BuyerId);

        base.OnModelCreating(builder);
    }
}
