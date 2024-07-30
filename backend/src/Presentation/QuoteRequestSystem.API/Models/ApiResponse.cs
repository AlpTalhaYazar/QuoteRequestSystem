using System.Text.Json.Serialization;

namespace QuoteRequestSystem.API.Models;

public class ApiResponse<T>
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
    [JsonPropertyName("data")]
    public T Data { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("errors")]
    public string[] Errors { get; set; }
    
    public ApiResponse()
    {
        IsSuccess = false;
        StatusCode = 0;
        Data = default;
        Message = string.Empty;
        Errors = Array.Empty<string>();
    }
    
    public ApiResponse(bool isSuccess, int statusCode, T data, string message, string[] errors)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Data = data;
        Message = message;
        Errors = errors;
    }
    
    public static ApiResponse<T> SuccessResponse(T data, string message = "")
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = data,
            Message = message
        };
    }
    
    public static ApiResponse<T> ErrorResponse(string message, string[] errors, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Data = default,
            Message = message,
            Errors = errors
        };
    }
}