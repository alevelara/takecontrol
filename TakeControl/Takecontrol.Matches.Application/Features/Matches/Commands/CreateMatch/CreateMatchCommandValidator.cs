using FluentValidation;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;

public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
{
    public CreateMatchCommandValidator()
    {
        RuleFor(m => m.ReservationId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Reservation can not be empty");

        RuleFor(m => m.PlayerId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Player can not be empty");
    }
}
