using FluentValidation;

namespace Takecontrol.User.Application.Features.Clubs.Commands.CancelForcedMatch;

public class CancelForcedMatchCommandValidator : AbstractValidator<CancelForcedMatchCommand>
{
    public CancelForcedMatchCommandValidator()
    {
        RuleFor(x => x.ClubId).NotEmpty()
            .WithMessage("Club can not be empty");

        RuleFor(x => x.MatchId).NotEmpty()
            .WithMessage("Match can not be empty");

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description can not be empty");
    }
}
