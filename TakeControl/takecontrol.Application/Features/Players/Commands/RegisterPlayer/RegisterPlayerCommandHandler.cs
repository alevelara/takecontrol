using MediatR;
using Microsoft.Extensions.Logging;
using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.ApplicationUser.Enum;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Features.Players.Commands.RegisterPlayer;

public class RegisterPlayerCommandHandler : ICommandHandler<RegisterPlayerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ILogger<RegisterPlayerCommandHandler> _logger;

    public RegisterPlayerCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, ILogger<RegisterPlayerCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _logger = logger;
    }

    public async Task<Unit> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
    {
        var userId = await this.RegisterPlayer(request.Name, request.Email, request.Password);

        var player = Player.Create(userId, request.Name, request.NumberOfClassesInAWeek, request.AvgNumberOfMatchesInAWeek, request.NumberOfYearsPlayed);
        var playerWriteRepository = _unitOfWork.Repository<Player>();
        await playerWriteRepository.AddAsync(player);

        await _unitOfWork.CompleteAsync();

        _logger.LogInformation($"New player {request.Name} has been registered succesfully.");

        return Unit.Value;
    }

    private async Task<Guid> RegisterPlayer(string name, string email, string password)
    {
        var registerRequest = new RegistrationRequest(name, email, password, UserType.Player);
        return await _authService.Register(registerRequest);
    }
}
