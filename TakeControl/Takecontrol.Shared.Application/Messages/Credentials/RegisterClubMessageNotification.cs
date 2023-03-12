using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Events.Credentials;

public sealed record class RegisterClubMessageNotification(string Name, string Email, string Password) : IEventNotification;
