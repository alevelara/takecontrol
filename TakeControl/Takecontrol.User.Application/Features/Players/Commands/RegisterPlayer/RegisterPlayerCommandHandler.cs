using MediatR;
using Microsoft.Extensions.Logging;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Features.Players.Commands.RegisterPlayer;

public class RegisterPlayerCommandHandler : ICommandHandler<RegisterPlayerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly ILogger<RegisterPlayerCommandHandler> _logger;

    public RegisterPlayerCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<RegisterPlayerCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Unit> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
    {
        var userId = await RegisterPlayer(request.Name, request.Email, request.Password);

        var player = Player.Create(userId, request.Name, request.NumberOfClassesInAWeek, request.AvgNumberOfMatchesInAWeek, request.NumberOfYearsPlayed);
        var playerWriteRepository = _unitOfWork.Repository<Player>();
        await playerWriteRepository.AddAsync(player);

        await _unitOfWork.CompleteAsync();

        await SendWelcomeEmail(request.Email, cancellationToken);

        _logger.LogInformation($"New player {request.Name} has been registered succesfully.");

        return Unit.Value;
    }

    private async Task<Guid> RegisterPlayer(string name, string email, string password)
    {
        var registerRequest = new RegisterPlayerMessageNotification(name, email, password);
        return await _mediator.Send(registerRequest);
    }

    private async Task SendWelcomeEmail(string toEmail, CancellationToken cancellationToken)
    {
        var message = new SendWelcomeEmailMessageNotification(toEmail);
        await _mediator.Publish(message);
    }
}
