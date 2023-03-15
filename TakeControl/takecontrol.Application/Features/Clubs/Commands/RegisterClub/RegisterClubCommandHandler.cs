using MediatR;
using Microsoft.Extensions.Logging;
using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Identity;
using Takecontrol.Application.Contracts.Persitence.Primitives;
using Takecontrol.Application.Services.Emails;
using Takecontrol.Domain.Messages.Identity;
using Takecontrol.Domain.Models.Addresses;
using Takecontrol.Domain.Models.ApplicationUser.Enum;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Domain.Models.Emails;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.Application.Features.Clubs.Commands.RegisterClub;

public sealed class RegisterClubCommandHandler : ICommandHandler<RegisterClubCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ILogger<RegisterClubCommandHandler> _logger;
    private readonly ISendEmailService _emailSender;

    public RegisterClubCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, ILogger<RegisterClubCommandHandler> logger, ISendEmailService emailSender)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(RegisterClubCommand request, CancellationToken cancellationToken)
    {
        var userId = await this.RegisterClub(request.Name, request.Email, request.Password);

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
        var registerRequest = new RegistrationRequest(name, email, password, UserType.Club);
        return await _authService.Register(registerRequest);
    }

    private async Task SendWelcomeEmail(string toEmail, CancellationToken cancellationToken)
    {
        
    }
}
