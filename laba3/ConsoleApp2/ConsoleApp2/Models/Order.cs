public class Order
{
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public int OwnerID { get; set; }
    public int EmployeeID { get; set; }

    // Навігаційні властивості
    public virtual Owner Owner { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } // Замовлення може містити багато продуктів
}
