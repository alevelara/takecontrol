using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Messages.Matches;

public sealed record class PlayerUnsusbscribed(string PlayerName, Guid MatchId) : ICommand<Unit>;
