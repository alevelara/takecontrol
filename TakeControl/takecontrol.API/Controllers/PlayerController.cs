using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using takecontrol.API.Routes;
using takecontrol.Application.Features.Players.Commands.JoinToClub;
using takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;
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

    [Authorize(Roles = Role.Player)]
<<<<<<< HEAD
    [HttpGet]
    public async Task<ActionResult<PlayerDto>> GetPlayer([FromQuery] Guid playerId)
    {
        var player = await _mediator.Send(new GetPlayerByIdQuery(playerId));
        return Ok(_mapper.From(player)
            .AdaptToType<PlayerDto>());
    }

    [Authorize(Roles = Role.Player)]
=======
>>>>>>> e4c019e8054464869312e69f9d9156c4d6aeb5c5
    [HttpPost(nameof(PlayerRouteName.Join))]
    public async Task<ActionResult> JoinToClub(JoinToClubRequest request)
    {
        var command = _mapper.Map<JoinToClubCommand>(request);
        await _mediator.Send(command);

        return StatusCode((int)HttpStatusCode.Created);
    }
}
