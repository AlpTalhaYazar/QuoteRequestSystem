namespace QuoteRequestSystem.Application.DTOs;

public class UserJWTTokenGenerationDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}