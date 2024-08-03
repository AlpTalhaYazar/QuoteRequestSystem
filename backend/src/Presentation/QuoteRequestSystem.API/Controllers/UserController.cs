using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuoteRequestSystem.API.Models;
using QuoteRequestSystem.Application.DTOs;
using QuoteRequestSystem.Application.Helper;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/offer")]
public class UserController(IServiceFactory serviceFactory, IConfiguration configuration) : ControllerBase
{
    private readonly IUserService _userService = serviceFactory.CreateUserService();
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();

        return new ApiResponse<IEnumerable<User>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = users,
            Message = "Users found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<User>> GetUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        return new ApiResponse<User>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = user,
            Message = "User found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost("sign-up")]
    public async Task<ApiResponse<User>> SignUp([FromBody] UserSignUpDto userSignUpDto)
    {
        (string passwordHash, string passwordSalt) = await PasswordHasher.CreatePasswordHash(userSignUpDto.Password);
        var user = new User
        {
            Email = userSignUpDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            FirstName = userSignUpDto.FirstName,
            LastName = userSignUpDto.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userService.AddAsync(user);

        return new ApiResponse<User>
        {
            IsSuccess = true,
            StatusCode = 201,
            Data = user,
            Message = "User created",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost("sign-in")]
    public async Task<ApiResponse<string>> SignIn([FromBody] UserSignInDto userSignInDto)
    {
        var userFromDb = await _userService.GetByEmailAsync(userSignInDto.Email);

        if (userFromDb is null)
        {
            return new ApiResponse<string>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = String.Empty,
                Message = "User not found",
                Errors = ["User not found"]
            };
        }

        if (userFromDb.PasswordHash != (await PasswordHasher.CreatePasswordHash(userSignInDto.Password)).hash)
        {
            return new ApiResponse<string>
            {
                IsSuccess = false,
                StatusCode = 401,
                Data = String.Empty,
                Message = "Invalid password",
                Errors = ["Invalid password"]
            };
        }

        userFromDb.LastSignedInAt = DateTime.UtcNow;
        await _userService.UpdateAsync(userFromDb);

        var token = GenerateJwtToken(userFromDb);

        return new ApiResponse<string>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = token,
            Message = "User signed in",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse<User>> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return new ApiResponse<User>
            {
                IsSuccess = false,
                StatusCode = 400,
                Data = null,
                Message = "Id mismatch",
                Errors = ["Id mismatch"]
            };
        }

        user.UpdatedAt = DateTime.UtcNow;

        await _userService.UpdateAsync(user);

        return new ApiResponse<User>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = user,
            Message = "User updated",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse<bool>> DeleteUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = false,
                Message = "User not found",
                Errors = ["User not found"]
            };
        }

        user.UpdatedAt = DateTime.UtcNow;
        user.DeletedAt = DateTime.UtcNow;
        user.IsDeleted = true;

        await _userService.UpdateAsync(user);

        return new ApiResponse<bool>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = true,
            Message = "User deleted",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("hard/{id}")]
    public async Task<ApiResponse<bool>> HardDeleteUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = false,
                Message = "User not found",
                Errors = ["User not found"]
            };
        }

        await _userService.DeleteAsync(id);

        return new ApiResponse<bool>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = true,
            Message = "User hard deleted",
            Errors = Array.Empty<string>()
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}