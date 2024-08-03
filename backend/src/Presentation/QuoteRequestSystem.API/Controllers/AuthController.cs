using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuoteRequestSystem.API.Filters;
using QuoteRequestSystem.API.Models;
using QuoteRequestSystem.Application.DTOs;
using QuoteRequestSystem.Core.Services;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/auth")]
public class AuthController(IServiceFactory serviceFactory, IConfiguration configuration) : ControllerBase
{
    private readonly IUserService _userService = serviceFactory.CreateUserService();
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("register")]
    public async Task<ApiResponse<UserSignInResponseDto>> Register([FromBody] UserSignUpDto model)
    {
        var user = await _userService.RegisterNewUserAsync(model.Email, model.Password, model.FirstName,
            model.LastName);

        if (user.UserId == 0 || String.IsNullOrWhiteSpace(user.Email) || String.IsNullOrWhiteSpace(user.Role))
            return new ApiResponse<UserSignInResponseDto>()
            {
                IsSuccess = false,
                StatusCode = 400,
                Data = null,
                Message = "User registration failed",
                Errors = new[] { "User registration failed" }
            };

        var (token, expireAt) = GenerateJwtToken(new UserJWTTokenGenerationDto()
            { Id = user.UserId, Email = user.Email, Role = user.Role });

        SetJwtCookie(token, expireAt);

        return new ApiResponse<UserSignInResponseDto>()
        {
            IsSuccess = true,
            StatusCode = 201,
            Data = new()
            {
                FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Role = user.Role
            },
            Message = "User registration successful",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost("login")]
    public async Task<ApiResponse<bool>> Login([FromBody] UserSignInDto model)
    {
        var user = await _userService.AuthenticateAsync(model.Email, model.Password);
        if (user.Id == 0 || String.IsNullOrWhiteSpace(user.Email) || String.IsNullOrWhiteSpace(user.Role))
            return new ApiResponse<bool>()
            {
                IsSuccess = false,
                StatusCode = 401,
                Data = false,
                Message = "Invalid email or password",
                Errors = new[] { "Invalid email or password" }
            };

        var (token, expireAt) = GenerateJwtToken(new UserJWTTokenGenerationDto()
            { Id = user.Id, Email = user.Email, Role = user.Role });

        SetJwtCookie(token, expireAt);

        return new ApiResponse<bool>()
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = true,
            Message = "Login successful",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost("logout")]
    public async Task<ApiResponse<bool>> Logout()
    {
        Console.WriteLine("Logout");
        RemoveJwtCookie();

        return new ApiResponse<bool>()
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = true,
            Message = "Logout successful",
            Errors = Array.Empty<string>()
        };
    }

    [ServiceFilter(typeof(CustomAuthorizationFilter))]
    [HttpGet("check-session")]
    public ApiResponse<object> CheckSession()
    {
        Console.WriteLine("jwt:" + Request.Cookies["jwt"]);
        var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

        Console.WriteLine(User.FindFirst(ClaimTypes.Name)?.Value);

        var responseDto = new
        {
            isAuthenticated = isAuthenticated,
            username = isAuthenticated ? username : null
        };

        var response = new ApiResponse<object>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = responseDto,
            Message = isAuthenticated
                ? $"Session for {username} is active"
                : "No active session",
            Errors = Array.Empty<string>()
        };

        Console.WriteLine("Session check: {0}", isAuthenticated);

        return response;
    }

    private (string Token, DateTime expireAt) GenerateJwtToken(UserJWTTokenGenerationDto userDto)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userDto.Id.ToString()),
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Role, userDto.Role)
            },
            expires: DateTime.UtcNow.AddDays(Convert.ToDouble(jwtSettings["ExpireDays"])),
            signingCredentials: creds);

        return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }

    private void SetJwtCookie(string token, DateTime expireAt)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            Expires = DateTimeOffset.UtcNow.AddDays(3),
            SameSite = SameSiteMode.Strict,
        };

        Response.Cookies.Append("jwt", token, cookieOptions);
    }

    private void RemoveJwtCookie()
    {
        Response.Cookies.Delete("jwt");
    }
}