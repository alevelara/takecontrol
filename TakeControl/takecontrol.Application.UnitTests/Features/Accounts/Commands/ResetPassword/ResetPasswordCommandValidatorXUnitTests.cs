using takecontrol.Application.Features.Accounts.Commands.ResetPassword;

namespace takecontrol.Application.UnitTests.Features.Accounts.Commands.ResetPassword;

[Trait("Category", "UnitTests")]
public class ResetPasswordCommandValidatorXUnitTests : IClassFixture<ResetPasswordCommandValidatorXUnitTests>
{
    private readonly ResetPasswordCommandValidator _validator;

    public ResetPasswordCommandValidatorXUnitTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("", false, 2)]
    [InlineData(null, false, 2)]
    [InlineData("email", false, 1)]
    [InlineData("email@test.com", true, 0)]
    public void Validate_Should_ValidateEmailParam(string email, bool isValid, int countErrors)
    {
        var command = new ResetPasswordCommand(email, "Password123!", "Password124!");
        var result = _validator.Validate(command);

        Assert.Equal(isValid, result.IsValid);
        Assert.Equal(countErrors, result.Errors.Count);
    }

    [Theory]
    [InlineData("", false, 6)]
    [InlineData(null, false, 6)]
    [InlineData("PasswordWithMoreThan14.", false, 1)]
    [InlineData("Min6.", false, 1)]
    [InlineData("outupper123!", false, 1)]
    [InlineData("OUTLOWER123!", false, 1)]
    [InlineData("OutDigit12", false, 1)]
    [InlineData("OutNumbers!", false, 1)]
    [InlineData("Password123!", true, 0)]
    public void Validate_Should_ValidateNewPasswordParam(string newPassword, bool isValid, int countErrors)
    {
        var command = new ResetPasswordCommand("email@test.com", "Password123!", newPassword);
        var result = _validator.Validate(command);

        Assert.Equal(isValid, result.IsValid);
        Assert.Equal(countErrors, result.Errors.Count);
    }

    [Theory]
    [InlineData(null, false, 2)]
    [InlineData("", false, 1)]
    [InlineData("Password123!", true, 0)]
    [InlineData("password", true, 0)]
    public void Validate_Should_ValidateCurrentPasswordParam(string currentPassword, bool isValid, int countErrors)
    {
        var command = new ResetPasswordCommand("email@test.com", currentPassword, "Password123!");
        var result = _validator.Validate(command);

        Assert.Equal(isValid, result.IsValid);
        Assert.Equal(countErrors, result.Errors.Count);
    }
}
