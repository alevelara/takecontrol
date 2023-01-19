using MediatR;
using System.Windows.Input;
using takecontrol.Application.Abstractions.Mediatr;

namespace takecontrol.Application.Features.Accounts.Commands.ResetPassword;

public sealed record class ResetPasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : ICommand<Unit>
{
}
