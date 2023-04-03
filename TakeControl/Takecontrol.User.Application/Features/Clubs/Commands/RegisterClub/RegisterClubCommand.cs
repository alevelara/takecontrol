using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;

public sealed record class RegisterClubCommand(
    string Name,
    string City,
    string Province,
    string MainAddress,
    string Email,
    string Password
    ) : ICommand<Unit>;
