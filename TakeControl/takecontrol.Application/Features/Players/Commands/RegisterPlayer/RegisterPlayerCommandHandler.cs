using MediatR;
using Microsoft.Extensions.Logging;
using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Identity;
using Takecontrol.Application.Contracts.Persitence.Primitives;
using Takecontrol.Application.Services.Emails;
using Takecontrol.Domain.Messages.Identity;
using Takecontrol.Domain.Models.ApplicationUser.Enum;
using Takecontrol.Domain.Models.Emails;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.Application.Features.Players.Commands.RegisterPlayer;

public class RegisterPlayerCommandHandler : ICommandHandler<RegisterPlayerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ILogger<RegisterPlayerCommandHandler> _logger;
    private readonly ISendEmailService _emailSender;

    public RegisterPlayerCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, ILogger<RegisterPlayerCommandHandler> logger, ISendEmailService emailSender)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
    {
        var userId = await this.RegisterPlayer(request.Name, request.Email, request.Password);

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
        var registerRequest = new RegistrationRequest(name, email, password, UserType.Player);
        return await _authService.Register(registerRequest);
    }

    private async Task SendWelcomeEmail(string toEmail, CancellationToken cancellationToken)
    {
        var email = Email.Create(toEmail, "Welcome to Takecontrol", TemplateType.WELCOME);
        await _emailSender.SendEmailAsync(email, cancellationToken);
    }
}
