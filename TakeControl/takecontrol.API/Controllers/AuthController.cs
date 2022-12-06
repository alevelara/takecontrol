using MediatR;
using Microsoft.AspNetCore.Mvc;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        return Ok(await _mediator.Send(query));
    }
}
