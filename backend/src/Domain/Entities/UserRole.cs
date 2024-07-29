namespace QuoteRequestSystem.Domain.Entities;

public class UserRole
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public bool IsCustomer { get; set; }
    public bool IsSupplier { get; set; }
    public bool IsEmployee { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
}