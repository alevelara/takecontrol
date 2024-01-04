using System.Net;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takecontrol.API.Routes;
using Takecontrol.Credential.Infrastructure.Constants;
using Takecontrol.User.Application.Features.Players.Commands.CancelMatch;
using Takecontrol.User.Application.Features.Players.Commands.JoinToAMatch;
using Takecontrol.User.Application.Features.Players.Commands.JoinToClub;
using Takecontrol.User.Application.Features.Players.Commands.RegisterPlayer;
using Takecontrol.User.Application.Features.Players.Commands.UnregisterFromClub;
using Takecontrol.User.Application.Features.Players.Commands.UnsubscribeFromMatch;
using Takecontrol.User.Application.Features.Players.Queries.GetAllPlayersByClubId;
using Takecontrol.User.Application.Features.Players.Queries.GetPlayer;
using Takecontrol.User.Domain.Messages.Players.Dtos;
using Takecontrol.User.Domain.Messages.Players.Requests;

namespace Takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PlayerController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(nameof(PlayerRouteName.Register))]
    public async Task<ActionResult> RegisterClub([FromBody] RegisterPlayerRequest request)
    {
        var command = _mapper.Map<RegisterPlayerCommand>(request);
        await _mediator.Send(command);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [Authorize(Roles = Role.Club)]
    [HttpGet]
    public async Task<ActionResult<PlayerDto>> GetPlayer([FromQuery] Guid playerId)
    {
        var player = await _mediator.Send(new GetPlayerByIdQuery(playerId));
        return Ok(_mapper.From(player)
            .AdaptToType<PlayerDto>());
    }

    [Authorize(Roles = Role.Player)]
    [HttpPost(nameof(PlayerRouteName.Join))]
    public async Task<ActionResult> JoinToClub(JoinToClubRequest request)
    {
        var command = _mapper.Map<JoinToClubCommand>(request);
        await _mediator.Send(command);

        return StatusCode((int)HttpStatusCode.Created);
    }

    [Authorize(Roles = Role.Club)]
    [HttpGet(PlayerRouteName.AllByClubId)]
    public async Task<ActionResult<List<PlayerDto>>> GetPlayersByClubId([FromQuery] Guid clubId)
    {
        var players = await _mediator.Send(new GetAllPlayersByClubIdQuery(clubId));
        return Ok(_mapper.From(players)
            .AdaptToType<List<PlayerDto>>());
    }

    [Authorize(Roles = Role.Player)]
    [HttpPost(nameof(PlayerRouteName.JoinToMatch))]
    public async Task<ActionResult> JoinToMatch(JoinToAMatchRequest request)
    {
        var command = _mapper.Map<JoinToAMatchCommand>(request);
        await _mediator.Send(command);

        return StatusCode((int)HttpStatusCode.Created);
    }

    [Authorize(Roles = Role.Player)]
    [HttpPut(nameof(PlayerRouteName.CancelMatch))]
    public async Task<ActionResult> CancelMatch(CancelMatchByPlayerRequest request)
    {
        var command = _mapper.Map<CancelMatchCommand>(request);
        await _mediator.Send(command);

        return Ok("Match was successful cancelled.");
    }

    [Authorize(Roles = Role.Player)]
    [HttpPost(nameof(PlayerRouteName.Unsubscribe))]
    public async Task<ActionResult> UnsubscribeFromMatch(UnsubscribeFromMatchRequest request)
    {
        var command = _mapper.Map<UnsubscribeFromMatchCommand>(request);
        await _mediator.Send(command);

        return Ok("You were unsubscribed from the match.");
    }

    [Authorize(Roles = Role.Player)]
    [HttpPost(nameof(PlayerRouteName.Unregister))]
    public async Task<ActionResult> UnregisterFromClub(UnregisterFromClubRequest request)
    {
        var command = _mapper.Map<UnregisterFromClubCommand>(request);
        await _mediator.Send(command);

        return NoContent();
    }
}
