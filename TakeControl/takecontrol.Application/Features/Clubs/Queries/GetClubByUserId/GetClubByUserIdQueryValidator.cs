using FluentValidation;
using takecontrol.Application.Features.Clubs.Queries.GetByUserId;

namespace takecontrol.Application.Features.Clubs.Queries.GetClubById;

public class GetClubByUserIdQueryValidator : AbstractValidator<GetClubByUserIdQuery>
{
    public GetClubByUserIdQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id can not be empty");
    }
}
