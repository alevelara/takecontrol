using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using takecontrol.API.Routes;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Application.Features.Clubs.Queries.GetByUserId;
using takecontrol.Domain.Dtos.Clubs;
using takecontrol.Domain.Messages.Clubs;

namespace takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClubController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ClubController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(nameof(ClubRouteName.Register))]
    public async Task<ActionResult> RegisterClub([FromBody] RegisterClubRequest request)
    {
        var command = _mapper.Map<RegisterClubCommand>(request);
        await _mediator.Send(command);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet]
    public async Task<ActionResult<ClubDto>> GetByUserId([FromQuery] Guid userId)
    {
        var query = new GetClubByUserIdQuery(userId);
        var club = await _mediator.Send(query);
        return Ok(_mapper.From(club)
            .AdaptToType<ClubDto>());
    }
}
