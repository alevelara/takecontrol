using FluentValidation;
using takecontrol.Domain.Utils;

namespace takecontrol.Application.Features.Players.Commands.JoinToClub;

public class JoinToClubCommandValidator : AbstractValidator<JoinToClubCommand>
{
    public JoinToClubCommandValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage("Code can not be empty")
            .Must(x => ValitatorsUtil.HasTheCorrectSize(x, 5))
            .WithMessage("Code must have 5 digits");

        RuleFor(c => c.ClubId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("ClubId can not be empty");

        RuleFor(c => c.PlayerId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("PlayerId can not be empty");
    }
}
