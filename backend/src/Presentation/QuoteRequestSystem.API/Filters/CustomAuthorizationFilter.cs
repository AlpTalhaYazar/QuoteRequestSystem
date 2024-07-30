using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuoteRequestSystem.API.Models;

namespace QuoteRequestSystem.API.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            context.Result = new ContentResult()
            {
                Content = System.Text.Json.JsonSerializer.Serialize(new ApiResponse<bool>()
                {
                    IsSuccess = false, StatusCode = 401, Data = false, Message = "Custom authorization failed",
                    Errors = new[] { "Custom authorization failed" }
                }),
                ContentType = "application/json",
                StatusCode = 401
            };
        }
    }
}