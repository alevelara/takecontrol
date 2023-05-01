using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Shared.Application.Events.Credentials;

public sealed record class RegisterPlayerMessageNotification(string Name, string Email, string Password) : ICommand<Guid>;