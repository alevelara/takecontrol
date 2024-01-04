using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Players.Commands.UnregisterFromClub;

public sealed record class UnregisterFromClubCommand(Guid UserId, Guid ClubId) : ICommand<Unit>;
