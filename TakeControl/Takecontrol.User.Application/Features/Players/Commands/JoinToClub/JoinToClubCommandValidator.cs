﻿using FluentValidation;
using Takecontrol.Shared.Domain.Utils;

namespace Takecontrol.User.Application.Features.Players.Commands.JoinToClub;

public class JoinToClubCommandValidator : AbstractValidator<JoinToClubCommand>
{
    public JoinToClubCommandValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage("Code can not be empty")
            .Must(x => ValitatorsUtil.HasTheCorrectSize(x, 5))
            .WithMessage("Code must have 5 digits");

        RuleFor(c => c.UserClubId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("ClubId can not be empty");

        RuleFor(c => c.UserPlayerId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("PlayerId can not be empty");
    }
}
