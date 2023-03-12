using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Credential.Application.Features.Accounts.Commands.ResetPassword;

public sealed record class ResetPasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : ICommand<Unit>
{
}
