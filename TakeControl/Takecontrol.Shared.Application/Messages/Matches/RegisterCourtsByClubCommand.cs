using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Messages.Matches;

public sealed record class RegisterCourtsByClubCommand(Guid ClubId, int NumberOfCourts) : ICommand<Unit>;
