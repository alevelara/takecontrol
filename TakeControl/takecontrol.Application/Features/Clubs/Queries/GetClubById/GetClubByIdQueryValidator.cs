using FluentValidation;
using takecontrol.Application.Features.Clubs.Queries.GetById;

namespace takecontrol.Application.Features.Clubs.Queries.GetClubById;

public class GetClubByIdQueryValidator : AbstractValidator<GetClubByIdQuery>
{
	public GetClubByIdQueryValidator()
	{
		RuleFor(c => c.Id)
			.NotNull()
			.NotEmpty()
			.NotEqual(Guid.Empty)
			.WithMessage("Id can not be empty");
	}
}
