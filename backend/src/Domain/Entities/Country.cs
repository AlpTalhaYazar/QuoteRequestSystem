namespace QuoteRequestSystem.Domain.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<City> Cities { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}