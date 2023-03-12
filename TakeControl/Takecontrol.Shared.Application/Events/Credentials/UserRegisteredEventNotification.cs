using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Events.Credentials;

public sealed record class UserRegisteredEventNotification(Guid id) : IEventNotification;