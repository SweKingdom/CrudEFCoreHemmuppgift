namespace CrudEFCoreHemmuppgift.Models;
using Microsoft.EntityFrameworkCore;


public class ShopContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderRow> OrderRows => Set<OrderRow>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(AppContext.BaseDirectory, "shop.db");

        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(e =>
        {
            // Sätter primär nyckel
            e.HasKey(x => x.CustomerId);

            // Säkerställer samma regler som data annotatuibs ( required + MaxLength)
            e.Property(x => x.Name)
                .IsRequired().HasMaxLength(100);
            e.Property(x => x.Email)
                .IsRequired().HasMaxLength(250);
            e.Property(x => x.City).HasMaxLength(250);
            e.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Order>(e =>
            {
                // PK
                e.HasKey(x => x.OrderId);

                e.Property(x => x.OrderDate);
                e.Property(x => x.Status).IsRequired().HasMaxLength(50);

                e.HasOne(x => x.Customer)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

            }
        );

        modelBuilder.Entity<OrderRow>(e =>
        {
            // PK
            e.HasKey(x => x.OrderRowId);

            e.Property(x => x.Quantity).IsRequired();
            e.Property(x => x.UnitPrice).IsRequired();
            e.HasOne(x => x.Order)
                .WithMany(x => x.OrderRows)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.HasKey(x => x.ProductId);

            e.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(100);
            e.Property(x => x.Pris)
                .IsRequired();
        });
    }
}