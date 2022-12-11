using FluentValidation;

namespace takecontrol.Application.Features.Clubs.Commands.RegisterClub;

public class RegisterClubCommandValidator : AbstractValidator<RegisterClubCommand>
{
	public RegisterClubCommandValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty()
			.WithMessage("Club Name can not be empty");

		RuleFor(c => c.City)
			.NotEmpty()
			.WithMessage("Club city can not be empty");

        RuleFor(c => c.MainAddress)
            .NotEmpty()
            .WithMessage("Club address can not be empty");

        RuleFor(c => c.Province)
            .NotEmpty()
            .WithMessage("Club province can not be empty");

    }   
}
