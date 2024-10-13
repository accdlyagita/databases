public class OrderProduct
{
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }

    // Навігаційні властивості
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}
