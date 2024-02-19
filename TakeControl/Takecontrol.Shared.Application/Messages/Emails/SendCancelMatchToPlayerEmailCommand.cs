using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Dtos.Matches;

namespace Takecontrol.Shared.Application.Events.Emails;

public sealed record class SendCancelMatchToPlayerEmailCommand(string EmailTo, string playerName, MatchDto match) : ICommand<Unit>;
