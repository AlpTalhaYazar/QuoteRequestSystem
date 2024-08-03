using System.Text.Json.Serialization;

namespace QuoteRequestSystem.Application.DTOs;

public class QuoteDto
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    [JsonPropertyName("mode")]
    public string Mode { get; set; }
    [JsonPropertyName("movement_type")]
    public string MovementType { get; set; }
    [JsonPropertyName("incoterms")]
    public string Incoterms { get; set; }
    [JsonPropertyName("country_id")]
    public int CountryId { get; set; }
    [JsonPropertyName("city_id")]
    public int CityId { get; set; }
    [JsonPropertyName("package_type")]
    public string PackageType { get; set; }
    [JsonPropertyName("package_dimension_unit")]
    public string PackageDimensionUnit { get; set; }
    [JsonPropertyName("package_amount")]
    public short PackageAmount { get; set; }
    [JsonPropertyName("weight_unit")]
    public string WeightUnit { get; set; }
    [JsonPropertyName("weight_value")]
    public int WeightValue { get; set; }
    [JsonPropertyName("currency_unit")]
    public string CurrencyUnit { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
    [JsonPropertyName("deleted_at")]
    public DateTime DeletedAt { get; set; }
    [JsonPropertyName("is_deleted")]
    public bool IsDeleted { get; set; }
}