using MediatR;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;

public class RegisterCourtsByClubCommandHandler : ICommandHandler<RegisterCourtsByClubCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncWriteRepository<Court> _courtWriteRepository;

    public RegisterCourtsByClubCommandHandler(IUnitOfWork unitOfWork, IAsyncWriteRepository<Court> courtWriteRepository)
    {
        _unitOfWork = unitOfWork;
        _courtWriteRepository = courtWriteRepository;
    }

    public async Task<Unit> Handle(RegisterCourtsByClubCommand request, CancellationToken cancellationToken)
    {
        var courts = new List<Court>();

        for (int i = 1; i <= request.NumberOfCourts; i++)
        {
            courts.Add(Court.Create(request.ClubId, $"Pista {i}"));
        }

        await _courtWriteRepository.AddRangeAsync(courts);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
