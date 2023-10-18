using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Messages.Matches;

public sealed record class CancelForcedMatchByClubCommand(Guid ClubId, Guid MatchId, string Description) : ICommand<Unit>;
