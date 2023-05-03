using FluentValidation;

namespace Takecontrol.User.Application.Features.Players.Queries.GetPlayer;

public class GetPlayerByIdQueryValidator : AbstractValidator<GetPlayerByIdQuery>
{
    public GetPlayerByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id can not be empty");
    }
}
