using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Messages.Matches;

public sealed record class UnsubscribePlayerFromMatchCommand(Guid PlayerId, Guid MatchId) : ICommand<Unit>;