using MediatR;
using Microsoft.Extensions.Logging;
using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Features.Clubs.Commands.RegisterClub;

public sealed class RegisterClubCommandHandler : ICommandHandler<RegisterClubCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly ILogger<RegisterClubCommandHandler> _logger;

    public RegisterClubCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, ILogger<RegisterClubCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _logger = logger;
    }

    public async Task<Unit> Handle(RegisterClubCommand request, CancellationToken cancellationToken)
    {
        var registerRequest = new RegistrationRequest(request.Name, request.Email, request.Password);
        await _authService.RegisterClub(registerRequest);

        var address = Address.Create(request.City, request.Province, request.MainAddress);
        var addressWriteRepository = _unitOfWork.Repository<Address>();
        await addressWriteRepository.AddAsync(address);

        //Add validation by name before adding the club?
        var club = Club.Create(address.Id, request.Name, GenerateRandomCodeForClub());
        var clubWriteRepository = _unitOfWork.Repository<Club>();
        await clubWriteRepository.AddAsync(club);

        await _unitOfWork.CompleteAsync();

        _logger.LogInformation($"New club {request.Name} has been registered succesfully.");

        return Unit.Value;
    }

    private string GenerateRandomCodeForClub()
    {
        return RandomGenerator.RandomString(5);
    }
}
