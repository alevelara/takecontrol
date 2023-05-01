using FluentValidation;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;

public class RegisterCourtsByClubCommandValidator : AbstractValidator<RegisterCourtsByClubCommand>
{
    public RegisterCourtsByClubCommandValidator()
    {
        RuleFor(c => c.ClubId)
            .Must(c => !c.Equals(Guid.Empty))
            .WithMessage("You should select a club");
    }
}
