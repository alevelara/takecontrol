using MediatR;
using takecontrol.Application.Abstractions.Mediatr;

namespace takecontrol.Application.Features.Accounts.Commands.UpdatePassword;

public sealed record class UpdatePasswordCommand(
    string Email,
    string NewPassword) : ICommand<Unit>
{
}
