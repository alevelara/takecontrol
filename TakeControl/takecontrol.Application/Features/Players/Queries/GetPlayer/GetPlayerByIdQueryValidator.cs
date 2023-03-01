using FluentValidation;
using takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;

namespace takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;

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
