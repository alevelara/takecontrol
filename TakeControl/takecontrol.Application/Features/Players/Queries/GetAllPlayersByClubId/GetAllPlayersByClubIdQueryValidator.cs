using FluentValidation;

namespace Takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId;

public class GetAllPlayersByClubIdQueryValidator : AbstractValidator<GetAllPlayersByClubIdQuery>
{
    public GetAllPlayersByClubIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Club Id can not be empty");
    }
}
