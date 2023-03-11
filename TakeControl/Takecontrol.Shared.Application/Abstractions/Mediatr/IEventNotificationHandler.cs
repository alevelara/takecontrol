using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Configuration.Abstractions.Mediatr;

public interface IEventNotificationHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : IEventNotification
{
}