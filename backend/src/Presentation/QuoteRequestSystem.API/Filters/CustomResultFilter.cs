using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuoteRequestSystem.API.Models;

namespace QuoteRequestSystem.API.Filters;

public class CustomResultFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult
            && objectResult.Value != null
            && objectResult.Value.GetType().IsGenericType
            && objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>))
        {
            var apiResponseType = typeof(ApiResponse<>).MakeGenericType(objectResult.Value.GetType().GetGenericArguments()[0]);
            var apiResponse = Convert.ChangeType(objectResult.Value, apiResponseType);

            context.Result = new ContentResult
            {
                Content = System.Text.Json.JsonSerializer.Serialize(apiResponse),
                ContentType = "application/json",
                StatusCode = (int)apiResponseType.GetProperty("StatusCode")!.GetValue(apiResponse)!
            };
        }
        else
        {
            context.Result = new ContentResult
            {
                Content = System.Text.Json.JsonSerializer.Serialize(ApiResponse<object>.ErrorResponse("Wrong response type", new[] { "Wrong response type" })),
                ContentType = "application/json",
                StatusCode = 400
            };
        }
    }
}