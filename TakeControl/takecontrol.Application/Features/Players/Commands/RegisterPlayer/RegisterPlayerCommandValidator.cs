using FluentValidation;
using takecontrol.Domain.Utils;

namespace takecontrol.Application.Features.Players.Commands.RegisterPlayer;

public class RegisterPlayerCommandValidator : AbstractValidator<RegisterPlayerCommand>
{
	public RegisterPlayerCommandValidator()
	{
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Club Name can not be empty")
            .NotNull()
            .WithMessage("Club Name can not be null");

        RuleFor(c => c.NumberOfClassesInAWeek)
            .NotEmpty()
            .WithMessage("Number of class in a week can not be empty")
            .NotNull()
            .WithMessage("Number of class in a week can not be null")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Number of class in a week can not be negative");

        RuleFor(c => c.AvgNumberOfMatchesInAWeek)
            .NotEmpty()
            .WithMessage("Average of number of matches in a week  can not be empty")
            .NotNull()
            .WithMessage("Average of number of matches in a week  can not be null")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Average of number of matches in a week can not be negative"); 

        RuleFor(c => c.NumberOfYearsPlayed)
            .NotEmpty()
            .WithMessage("Number of years played can not be empty")
            .NotNull()
            .WithMessage("Number of years played can not be null")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Number of years played can not be negative");

        RuleFor(c => c.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Invalid email.")
            .NotEmpty().WithMessage("User email can not be empty")
            .NotNull().WithMessage("User email can not be null");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password can not be empty")
            .NotNull()
            .WithMessage("Password can not be null")
            .MaximumLength(14)
            .WithMessage("Password size can not be higher than 14 characters")
            .MinimumLength(8)
            .WithMessage("Password size can not be smaller than 8 characters")
            .Must(x => ValitatorsUtil.HasAnyLowerCase(x))
            .WithMessage("Password must have any character in lowercase")
            .Must(x => ValitatorsUtil.HasAnyUpperCase(x))
            .WithMessage("Password must have any character in uppercase")
            .Must(x => ValitatorsUtil.HasAnyDigit(x))
            .WithMessage("Password must have any digit from 0 to 9")
            .Must(x => ValitatorsUtil.HasAnySymbol(x))
            .WithMessage("Password must have any symbol");
    }
}
