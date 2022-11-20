using Microsoft.AspNetCore.Mvc;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Domain.Mappings.Identity;

namespace takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        return Ok(await _authService.Login(request));
    }
}
