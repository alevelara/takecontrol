using FluentValidation;

namespace Takecontrol.User.Application.Features.Players.Queries.GetAllPlayersByClubId;

public class GetAllPlayersByClubIdQueryValidator : AbstractValidator<GetAllPlayersByClubIdQuery>
{
    public GetAllPlayersByClubIdQueryValidator()
    {
        RuleFor(c => c.ClubId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Club Id can not be empty");
    }
}
