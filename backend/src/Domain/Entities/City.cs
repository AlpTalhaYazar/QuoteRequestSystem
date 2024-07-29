namespace QuoteRequestSystem.Domain.Entities;

public class City
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}