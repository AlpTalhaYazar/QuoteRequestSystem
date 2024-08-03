using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuoteRequestSystem.Application.DTOs;

public class QuoteCreateDto
{
    [Required]
    [JsonPropertyName("mode")]
    public string Mode { get; set; }
    [Required]
    [JsonPropertyName("movement_type")]
    public string MovementType { get; set; }
    [Required]
    [JsonPropertyName("incoterms")]
    public string Incoterms { get; set; }
    [Required]
    [JsonPropertyName("country_id")]
    public int CountryId { get; set; }
    [Required]
    [JsonPropertyName("city_id")]
    public int CityId { get; set; }
    [Required]
    [JsonPropertyName("package_type")]
    public string PackageType { get; set; }
    [Required]
    [JsonPropertyName("package_dimension_unit")]
    public string PackageDimensionUnit { get; set; }
    [Required]
    [JsonPropertyName("package_weight_unit")]
    public double Width { get; set; }
    [Required]
    [JsonPropertyName("length")]
    public double Length { get; set; }
    [Required]
    [JsonPropertyName("height")]
    public double Height { get; set; }
    [Required]
    [JsonPropertyName("package_amount")]
    public short PackageAmount { get; set; }
    [Required]
    [JsonPropertyName("weight_unit")]
    public string WeightUnit { get; set; }
    [Required]
    [JsonPropertyName("weight_value")]
    public int WeightValue { get; set; }
    [Required]
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}