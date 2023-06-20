using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Takecontrol.API.Routes;
using Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;
using Takecontrol.Matches.Domain.Messages.Matches.Requests;

namespace Takecontrol.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MatchController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost(nameof(MatchRouteName.Create))]
    public async Task<ActionResult> CreateMatch([FromBody] CreateMatchRequest request)
    {
        var command = _mapper.Map<CreateMatchCommand>(request);
        await _mediator.Send(command);
        return StatusCode((int)HttpStatusCode.Created);
    }
}