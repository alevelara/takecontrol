namespace Takecontrol.Shared.Configuration.Abstractions.Mediatr;

public interface INotificationHandler<TNotification> : MediatR.INotificationHandler<TNotification>
    where TNotification : INotification
{
}
