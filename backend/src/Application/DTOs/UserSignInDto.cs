using System.ComponentModel.DataAnnotations;

namespace QuoteRequestSystem.Application.DTOs;

public class UserSignInDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email address is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}