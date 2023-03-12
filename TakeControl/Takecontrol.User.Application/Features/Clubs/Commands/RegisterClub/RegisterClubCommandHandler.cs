using MediatR;
using Microsoft.Extensions.Logging;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Configuration.Abstractions.Mediatr;
using Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;
using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs;

namespace takecontrol.Application.Features.Clubs.Commands.RegisterClub;

public sealed class RegisterClubCommandHandler :
    ICommandHandler<RegisterClubCommand, Unit>,
    IEventNotificationHandler<UserRegisteredEventNotification>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly ILogger<RegisterClubCommandHandler> _logger;

    public RegisterClubCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<RegisterClubCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Unit> Handle(RegisterClubCommand request, CancellationToken cancellationToken)
    {
        var userId = await this.RegisterClub(request.Name, request.Email, request.Password);
        _mediator.
        

        await SendWelcomeEmail(request.Email, cancellationToken);

        _logger.LogInformation($"New club {request.Name} has been registered succesfully.");

        return Unit.Value;
    }

    public async Task Handle(UserRegisteredEventNotification notification, CancellationToken cancellationToken)
    {
        var address = Address.Create(request.City, request.Province, request.MainAddress);
        var addressWriteRepository = _unitOfWork.Repository<Address>();
        await addressWriteRepository.AddAsync(address);

        var club = Club.Create(address.Id, userId, request.Name);
        var clubWriteRepository = _unitOfWork.Repository<Club>();
        await clubWriteRepository.AddAsync(club);

        await _unitOfWork.CompleteAsync();
    }

    private async Task<Guid> RegisterClub(string name, string email, string password)
    {
        var registerRequest = new RegistrationRequest(name, email, password, UserType.Club);
        return await _authService.Register(registerRequest);
    }

    private async Task SendWelcomeEmail(string toEmail, CancellationToken cancellationToken)
    {
        
    }
}
