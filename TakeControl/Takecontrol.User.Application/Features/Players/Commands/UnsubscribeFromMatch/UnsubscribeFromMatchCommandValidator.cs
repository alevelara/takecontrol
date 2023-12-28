using FluentValidation;

namespace Takecontrol.User.Application.Features.Players.Commands.UnsubscribeFromMatch;

public class UnsubscribeFromMatchCommandValidator : AbstractValidator<UnsubscribeFromMatchCommand>
{
    public UnsubscribeFromMatchCommandValidator()
    {
        RuleFor(c => c.MatchId)
            .NotEmpty()
            .WithMessage("Match can not be empty");

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage("Match can not be empty");
    }
}
