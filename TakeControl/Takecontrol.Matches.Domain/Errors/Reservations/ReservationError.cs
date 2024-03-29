﻿using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Errors.Match;

public sealed class ReservationError : DomainError
{
    public ReservationError(int codeId, string message) : base(codeId, message)
    {
    }

    public static ReservationError ReservationIsNotAvailable = new ReservationError(1901, "Reservation is not available.");
    public static ReservationError ReservationNotFound = new ReservationError(1902, "Reservation not found.");
    public static ReservationError ReservationNotCancellable = new ReservationError(1903, "This reservation is not cancellable due is closer to the start date");

}
