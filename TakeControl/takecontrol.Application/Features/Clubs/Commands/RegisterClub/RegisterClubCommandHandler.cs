using MediatR;
using Microsoft.Extensions.Logging;
using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Contracts.Persitence.Primitives;
using takecontrol.Application.Services.Emails;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.ApplicationUser.Enum;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.Application.Features.Clubs.Commands.RegisterClub;

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
