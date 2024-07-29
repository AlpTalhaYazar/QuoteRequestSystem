namespace QuoteRequestSystem.Domain.Entities;

public class Dimension
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Width { get; set; }
    public int Length { get; set; }
    public int Height { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}