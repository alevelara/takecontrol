using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using takecontrol.API.Routes;
using takecontrol.Application.Features.Accounts.Commands.ResetPassword;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(nameof(AuthRouteName.Login))]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        return Ok(await _mediator.Send(query));
    }

    [HttpPost(nameof(AuthRouteName.ResetPassword))]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var query = _mapper.Map<ResetPasswordCommand>(request);
        await _mediator.Send(query);
        return StatusCode((int)HttpStatusCode.Created);
    }
}
