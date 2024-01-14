using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Players.Commands.JoinToAMatch;

public sealed record class JoinToAMatchCommand(Guid UserId, Guid MatchId) : ICommand<Unit>;
