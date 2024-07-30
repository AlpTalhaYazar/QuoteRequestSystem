using System.ComponentModel.DataAnnotations;

namespace QuoteRequestSystem.Application.DTOs;

public class UserSignUpDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Email address is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }
}