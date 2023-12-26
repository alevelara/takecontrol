using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Players.Commands.UnsubscribeFromMatch;

public sealed record class UnsubscribeFromMatchCommand(Guid UserId, Guid MatchId) : ICommand<Unit>;