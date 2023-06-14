using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;

public record class CreateMatchCommand(Guid PlayerId, Guid ReservationId) : ICommand<Unit>
{
}
