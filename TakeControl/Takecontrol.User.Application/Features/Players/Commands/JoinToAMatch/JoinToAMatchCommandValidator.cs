using FluentValidation;

namespace Takecontrol.User.Application.Features.Players.Commands.JoinToAMatch;

public sealed class JoinToAMatchCommandValidator : AbstractValidator<JoinToAMatchCommand>
{
    public JoinToAMatchCommandValidator()
    {
        RuleFor(c => c.MatchId)
            .Must(v => !v.Equals(Guid.Empty))
            .WithMessage("Match id can not be empty");

        RuleFor(c => c.PlayerId)
            .Must(v => !v.Equals(Guid.Empty))
            .WithMessage("Player id can not be empty");
    }
}
