using System.Text.Json.Serialization;

namespace QuoteRequestSystem.Application.DTOs;

public class UserSignInResponseDto
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }
}