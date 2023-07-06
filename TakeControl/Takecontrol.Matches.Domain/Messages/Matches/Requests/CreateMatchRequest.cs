namespace Takecontrol.Matches.Domain.Messages.Matches.Requests;

public record class CreateMatchRequest(Guid PlayerId, Guid ReservationId)
{
}
