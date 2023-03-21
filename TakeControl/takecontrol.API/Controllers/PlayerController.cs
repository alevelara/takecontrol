using System.Net;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using takecontrol.API.Routes;
using takecontrol.Application.Features.Players.Commands.JoinToClub;
using takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;
using takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId;
using takecontrol.Domain.Dtos.Players;
using takecontrol.Domain.Messages.Players;
using takecontrol.Identity.Constants;

namespace takecontrol.API.Controllers;

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
}
