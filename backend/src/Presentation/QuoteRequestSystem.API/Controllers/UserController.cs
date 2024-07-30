using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/offer")]
public class UserController : ControllerBase
{
}