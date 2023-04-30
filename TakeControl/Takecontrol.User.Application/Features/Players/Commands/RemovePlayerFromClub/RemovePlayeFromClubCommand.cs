using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Players.Commands.RemovePlayeFromClub;

public class RemovePlayerFromClubCommand : ICommand<Unit>
{
    public Guid PlayerId { get; private set; }

    public Guid ClubId { get; private set; }

    public RemovePlayerFromClubCommand(Guid playerId, Guid clubId)
    {
        PlayerId = playerId;
        ClubId = clubId;
    }
}
