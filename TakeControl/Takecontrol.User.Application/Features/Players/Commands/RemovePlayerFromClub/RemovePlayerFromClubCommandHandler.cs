using MediatR;
using Microsoft.Extensions.Logging;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Features.Players.Commands.RemovePlayeFromClub;

public class RemovePlayerFromClubCommandHandler : ICommandHandler<RemovePlayerFromClubCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly ILogger<RemovePlayerFromClubCommandHandler> _logger;

    public RemovePlayerFromClubCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<RemovePlayerFromClubCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Unit> Handle(RemovePlayerFromClubCommand request, CancellationToken cancellationToken)
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
        await _mediator.Send(message, cancellationToken);
    }
}
