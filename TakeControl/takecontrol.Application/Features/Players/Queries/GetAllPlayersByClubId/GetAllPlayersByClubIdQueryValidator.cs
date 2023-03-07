using FluentValidation;
using takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;

namespace takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId;

public class GetAllPlayersByClubIdQueryValidator : AbstractValidator<GetAllPlayersByClubIdQuery>
{
    public GetAllPlayersByClubIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id can not be empty");
    }
}
