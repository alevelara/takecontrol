using FluentValidation;
using Takecontrol.Shared.Domain.Utils;

namespace Takecontrol.User.Application.Features.Players.Commands.RemovePlayeFromClub;

public class RemovePlayerFromClubCommandValidator : AbstractValidator<RemovePlayerFromClubCommand>
{
    public RemovePlayerFromClubCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty()
            .WithMessage("PlayerId can not be empty")
            .NotNull()
            .WithMessage("PlayerId can not be null");

        RuleFor(c => c.ClubId)
            .NotEmpty()
            .WithMessage("ClubId can not be empty")
            .NotNull()
            .WithMessage("ClubId can not be null");
    }
}
