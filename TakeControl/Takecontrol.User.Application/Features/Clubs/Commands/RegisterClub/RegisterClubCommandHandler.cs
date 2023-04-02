using MediatR;
using Microsoft.Extensions.Logging;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs;

namespace Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;

public sealed class RegisterClubCommandHandler :
    ICommandHandler<RegisterClubCommand, Unit>
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
        var userId = await RegisterClub(request.Name, request.Email, request.Password);

        var address = Address.Create(request.City, request.Province, request.MainAddress);
        var addressWriteRepository = _unitOfWork.Repository<Address>();
        await addressWriteRepository.AddAsync(address);

        var club = Club.Create(address.Id, userId, request.Name);
        var clubWriteRepository = _unitOfWork.Repository<Club>();
        await clubWriteRepository.AddAsync(club);

        await _unitOfWork.CompleteAsync();

        await SendWelcomeEmail(request.Email, cancellationToken);

        _logger.LogInformation($"New club {request.Name} has been registered succesfully.");

        return Unit.Value;
    }

    private async Task<Guid> RegisterClub(string name, string email, string password)
    {
        var message = new RegisterClubMessageNotification(name, email, password);
        return await _mediator.Send(message);
    }

    private async Task SendWelcomeEmail(string toEmail, CancellationToken cancellationToken)
    {
        var eventNotification = new SendWelcomeEmailMessageNotification(toEmail);
        await _mediator.Send(eventNotification, cancellationToken);
    }
}
