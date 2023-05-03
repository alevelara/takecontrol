using MediatR;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;

public class RegisterCourtsByClubCommandHandler : ICommandHandler<RegisterCourtsByClubCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCourtsByClubCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RegisterCourtsByClubCommand request, CancellationToken cancellationToken)
    {
        var courts = new List<Court>();

        for (int i = 1; i <= request.NumberOfCourts; i++)
        {
            courts.Add(Court.Create(request.ClubId, $"Pista {i}"));
        }

        await _unitOfWork.Repository<Court>().AddRangeAsync(courts);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
