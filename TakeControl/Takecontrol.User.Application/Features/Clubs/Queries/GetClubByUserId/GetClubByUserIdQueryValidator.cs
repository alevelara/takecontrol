using FluentValidation;

namespace Takecontrol.User.Application.Features.Clubs.Queries.GetClubByUserId;

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
