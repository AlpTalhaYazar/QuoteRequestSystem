namespace QuoteRequestSystem.Domain.Entities;

public class Quote
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Mode { get; set; }
    public string MovementType { get; set; }
    public string Incoterms { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public string PackageType { get; set; }
    public string PackageDimensionUnit { get; set; }
    public short PackageAmount { get; set; }
    public string WeightUnit { get; set; }
    public int WeightValue { get; set; }
    public string CurrencyUnit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public Offer Offer { get; set; }
}