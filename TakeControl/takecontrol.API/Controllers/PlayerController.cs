using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using takecontrol.Domain.Messages.Clubs;
using takecontrol.Domain.Messages.Players;

namespace takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PlayerController : ControllerBase
{
    private IMediator _mediator;

    public PlayerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterClub([FromBody] RegisterPlayerRequest request)
    {
        var command = new RegisterPlayerCommand(request.Name, request.Email, request.Password, request.NumberOfClassesInAWeek, request.AvgNumberOfMatchesInAWeek, request.NumberOfYearsPlayed);
        await _mediator.Send(command);
        return StatusCode((int)HttpStatusCode.Created);
    }
}
