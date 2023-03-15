using MediatR;
using Takecontrol.Application.Abstractions.Mediatr;

namespace Takecontrol.Application.Features.Players.Commands.JoinToClub;

public sealed record class JoinToClubCommand(Guid PlayerId, Guid ClubId, string Code) : ICommand<Unit>
{
}
