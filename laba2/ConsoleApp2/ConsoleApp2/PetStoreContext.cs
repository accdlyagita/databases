using Microsoft.EntityFrameworkCore;

public class PetStoreContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Визначення складеного первинного ключа для OrderProduct
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderID, op.ProductID });

        // Налаштування зв'язків
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductID)
            .OnDelete(DeleteBehavior.Cascade);

        // Визначення типу даних для Price
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10, 2")
            .HasPrecision(10, 2); // Визначаємо точність і масштаб для decimal
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-00L2CO6\\SQLEXPRESS;Database=PetStoreDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
