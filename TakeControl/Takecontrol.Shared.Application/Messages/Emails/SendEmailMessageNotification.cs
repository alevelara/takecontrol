using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Events.Emails;

public sealed record class SendWelcomeEmailMessageNotification(string EmailTo) : ICommand<Unit>
{
}
