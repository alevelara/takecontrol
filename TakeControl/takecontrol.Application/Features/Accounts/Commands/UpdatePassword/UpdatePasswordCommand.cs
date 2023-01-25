using MediatR;
using System.Windows.Input;
using takecontrol.Application.Abstractions.Mediatr;

namespace takecontrol.Application.Features.Accounts.Commands.UpdatePassword;

public sealed record class UpdatePasswordCommand(
    string Email,
    string NewPassword,
    string Token) : ICommand<Unit>
{
}
