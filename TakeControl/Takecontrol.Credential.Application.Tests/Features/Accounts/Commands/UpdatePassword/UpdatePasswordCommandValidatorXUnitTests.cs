using Takecontrol.Credential.Application.Features.Accounts.Commands.UpdatePassword;
using Takecontrol.Shared.Tests.Constants;
using Xunit;

namespace Takecontrol.Credential.Application.Tests.Features.Accounts.Commands.UpdatePassword;

[Trait("Category", Category.UnitTest)]
public class UpdatePasswordCommandValidatorXUnitTests : IClassFixture<UpdatePasswordCommandValidatorXUnitTests>
{
    private readonly UpdatePasswordCommandValidator _validator;

    public UpdatePasswordCommandValidatorXUnitTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("", false, 2)]
    [InlineData(null, false, 2)]
    [InlineData("email", false, 1)]
    [InlineData("@", false, 1)]
    [InlineData("@.com", false, 1)]
    [InlineData("email@test.com", true, 0)]
    public void Validate_Should_ValidateEmailParam(string email, bool isValid, int countErrors)
    {
        var command = new UpdatePasswordCommand(email, "Password124!");
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
    [InlineData("password", false, 3)]
    public void Validate_Should_ValidateNewPasswordParam(string newPassword, bool isValid, int countErrors)
    {
        var command = new UpdatePasswordCommand("email@test.com", newPassword);
        var result = _validator.Validate(command);

        Assert.Equal(isValid, result.IsValid);
        Assert.Equal(countErrors, result.Errors.Count);
    }
}
