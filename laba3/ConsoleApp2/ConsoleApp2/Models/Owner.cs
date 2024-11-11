public class Owner
{
    public int OwnerID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }

    // Навігаційні властивості
    public virtual ICollection<Order> Orders { get; set; } // Один власник може мати багато замовлень
}
