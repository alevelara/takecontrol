using MediatR;
using takecontrol.Application.Abstractions.Mediatr;

namespace takecontrol.Application.Features.Players.Commands.JoinToClub;

public sealed record class JoinToClubCommand(Guid PlayerId, Guid ClubId, string Code) : ICommand<Unit>
{
}
