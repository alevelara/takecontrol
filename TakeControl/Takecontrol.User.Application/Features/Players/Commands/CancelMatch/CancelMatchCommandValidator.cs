using FluentValidation;

namespace Takecontrol.User.Application.Features.Players.Commands.CancelMatch;

public sealed class CancelMatchCommandValidator : AbstractValidator<CancelMatchCommand>
{
    public CancelMatchCommandValidator()
    {
        RuleFor(c => c.MatchId)
            .Must(v => !v.Equals(Guid.Empty))
            .WithMessage("Match id can not be empty");

        RuleFor(c => c.UserId)
            .Must(v => !v.Equals(Guid.Empty))
            .WithMessage("Player id can not be empty");
    }
}
