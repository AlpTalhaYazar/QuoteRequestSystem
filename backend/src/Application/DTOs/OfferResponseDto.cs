using System.Text.Json.Serialization;

namespace QuoteRequestSystem.Application.DTOs;

public class OfferResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("quote_id")]
    public int QuoteId { get; set; }
    [JsonPropertyName("offer_currency_type")]
    public string OfferCurrencyType { get; set; }
    [JsonPropertyName("offer_amount")]
    public decimal OfferAmount { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}