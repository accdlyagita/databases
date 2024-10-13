public class Employee
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }

    // Навігаційні властивості
    public virtual ICollection<Order> Orders { get; set; } // Один працівник може обробляти багато замовлень
}
