using MediatR;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;

public class RegisterCourtsByClubCommandHandler : ICommandHandler<RegisterCourtsByClubCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private const int MATCH_DURATION = 90;

    public RegisterCourtsByClubCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RegisterCourtsByClubCommand request, CancellationToken cancellationToken)
    {
        var courts = new List<Court>();
        var reservations = new List<Reservation>();

        for (int i = 1; i <= request.NumberOfCourts; i++)
        {
            var court = Court.Create(request.ClubId, $"Pista {i}");
            reservations.AddRange(GenerateReservationsByHours(court.Id, request.OpenDate, request.ClosureDate));

            courts.Add(court);
        }

        await _unitOfWork.Repository<Court>().AddRangeAsync(courts);
        await _unitOfWork.Repository<Reservation>().AddRangeAsync(reservations);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }

    private List<Reservation> GenerateReservationsByHours(Guid courtId, TimeOnly openTime, TimeOnly closureTime)
    {
        var reservations = new List<Reservation>();
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        var numberOfReservationsByCourt = GetNumberOfCourtsFromOpenToCloseDate(openTime, closureTime);

        for (int days = 0; days <= 7; days++)
        {
            var reservationTime = openTime;
            for (int reservation = 0; reservation < numberOfReservationsByCourt; reservation++)
            {
                reservations.Add(Reservation.Create(courtId, reservationTime, reservationTime.AddMinutes(90), date.AddDays(days)));
                reservationTime = reservationTime.AddMinutes(90);
            }
        }

        return reservations;
    }

    private int GetNumberOfCourtsFromOpenToCloseDate(TimeOnly openTime, TimeOnly closureTime)
    {
        var minutesBetweenHours = (closureTime - openTime).TotalMinutes;
        return (int)(minutesBetweenHours / MATCH_DURATION);
    }
}
