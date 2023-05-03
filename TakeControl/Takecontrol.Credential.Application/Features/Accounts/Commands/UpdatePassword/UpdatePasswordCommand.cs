using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Credential.Application.Features.Accounts.Commands.UpdatePassword;

public sealed record class UpdatePasswordCommand(
    string Email,
    string NewPassword) : ICommand<Unit>
{
}
