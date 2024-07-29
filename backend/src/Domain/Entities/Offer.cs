namespace QuoteRequestSystem.Domain.Entities;

public class Offer
{
    public int Id { get; set; }
    public int QuoteId { get; set; }
    public Quote Quote { get; set; }
    public decimal Price { get; set; }
    public string CurrencyUnit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}