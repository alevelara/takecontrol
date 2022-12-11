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

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("User Email can not be empty");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password can not be empty")
            .MaximumLength(14)
            .WithMessage("Password size can not be higher than 14 characters")
            .MinimumLength(8)
            .WithMessage("Password size can not be smaller than 8 characters");            
    }   
}
