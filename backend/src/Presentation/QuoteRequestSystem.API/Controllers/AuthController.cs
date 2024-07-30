using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/auth")]
public class AuthController : ControllerBase
{
}