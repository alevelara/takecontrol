using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Players.Commands.JoinToClub;

public sealed record class JoinToClubCommand(Guid UserPlayerId, Guid UserClubId, string Code) : ICommand<Unit>
{
}
