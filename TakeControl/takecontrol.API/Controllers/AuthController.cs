using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using takecontrol.API.Routes;
using takecontrol.Application.Features.Accounts.Commands.ResetPassword;
using takecontrol.Application.Features.Accounts.Commands.UpdatePassword;
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

    [HttpPost(nameof(AuthRouteName.Login))]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        return Ok(await _mediator.Send(query));
    }

    [HttpPost(nameof(AuthRouteName.ResetPassword))]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var query = new ResetPasswordCommand(request.Email, request.CurrentPassword, request.NewPassword);
        await _mediator.Send(query);
        return StatusCode((int)HttpStatusCode.Created);

    }

    [HttpPost(nameof(AuthRouteName.UpdatePassword))]
    public async Task<ActionResult<AuthResponse>> UpdatePasword([FromBody] UpdatePasswordRequest request)
    {
        var query = new UpdatePasswordCommand(request.Email, request.NewPassword);
        await _mediator.Send(query);

        return StatusCode((int)HttpStatusCode.Created);
    }
}
