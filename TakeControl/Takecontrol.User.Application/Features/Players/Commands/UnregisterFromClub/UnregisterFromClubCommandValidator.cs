using FluentValidation;
using Takecontrol.Shared.Domain.Utils;

namespace Takecontrol.User.Application.Features.Players.Commands.UnregisterFromClub;

public class UnregisterFromClubCommandValidator : AbstractValidator<UnregisterFromClubCommand>
{
    public UnregisterFromClubCommandValidator()
    {
        RuleFor(c => c.UserPlayerId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("UserId can not be empty");

        RuleFor(c => c.UserClubId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("ClubId can not be empty");
    }
}
