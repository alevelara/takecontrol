using MediatR;
using Takecontrol.Application.Abstractions.Mediatr;

namespace Takecontrol.Application.Features.Accounts.Commands.UpdatePassword;

public sealed record class UpdatePasswordCommand(
    string Email,
    string NewPassword) : ICommand<Unit>
{
}
