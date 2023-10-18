using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Clubs.Commands.CancelForcedMatch;

public sealed record class CancelForcedMatchCommand(Guid ClubId, Guid MatchId, string Description) : ICommand<Unit>;
