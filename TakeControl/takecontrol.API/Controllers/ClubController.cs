using MediatR;
using Microsoft.AspNetCore.Mvc;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Domain.Messages.Clubs;

namespace takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClubController : ControllerBase
{
    private IMediator _mediator;

    public ClubController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterClub([FromBody] RegisterClubRequest request)
    {
        var command = new RegisterClubCommand(request.Name, request.City, request.Province, request.MainAddress, request.Email, request.Password);
        await _mediator.Send(command);
        return StatusCode(201);
    }
}
