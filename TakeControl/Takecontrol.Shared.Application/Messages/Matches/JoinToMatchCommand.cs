﻿using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Messages.Matches;

public sealed record class JoinToMatchCommand(Guid PlayerId, Guid MatchId) : ICommand<Unit>;