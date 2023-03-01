using MediatR;
using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Persitence.Clubs;
using takecontrol.Application.Contracts.Persitence.Primitives;
using takecontrol.Application.Exceptions;
using takecontrol.Domain.Errors.Clubs;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Domain.Models.PlayerClubs;

namespace takecontrol.Application.Features.Players.Commands.JoinToClub;

public sealed class JoinToClubCommandHandler : ICommandHandler<JoinToClubCommand, Unit>
{
    private readonly IClubReadRepository _clubReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JoinToClubCommandHandler(IClubReadRepository clubReadRepository, IUnitOfWork unitOfWork)
    {
        _clubReadRepository = clubReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(JoinToClubCommand request, CancellationToken cancellationToken)
    {
        var club = await _clubReadRepository.GetClubByCodeAndClubId(request.ClubId, request.Code);

        if (club == null)
            throw new NotFoundException(ClubError.ClubDoesnotMatchByCode);

        var association = PlayerClub.Create(request.PlayerId, request.ClubId);

        await _unitOfWork.Repository<PlayerClub>().AddAsync(association);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
