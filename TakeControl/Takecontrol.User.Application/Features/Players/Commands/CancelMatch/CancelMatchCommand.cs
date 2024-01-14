using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Players.Commands.CancelMatch;

public sealed record class CancelMatchCommand(Guid UserId, Guid MatchId) : ICommand<Unit>;