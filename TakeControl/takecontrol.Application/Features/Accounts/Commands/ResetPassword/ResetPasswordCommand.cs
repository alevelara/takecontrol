using System.Windows.Input;
using MediatR;
using Takecontrol.Application.Abstractions.Mediatr;

namespace Takecontrol.Application.Features.Accounts.Commands.ResetPassword;

public sealed record class ResetPasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : ICommand<Unit>
{
}
