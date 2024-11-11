public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    // Навігаційні властивості
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } // Один продукт може бути в багатьох замовленнях
}
