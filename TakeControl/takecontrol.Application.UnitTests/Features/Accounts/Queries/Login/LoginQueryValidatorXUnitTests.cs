using takecontrol.Application.Features.Accounts.Queries.Login;

namespace tekecontrol.Application.Tests.Features.Account.Queries.Login;

[Trait("Category", "UnitTests")]
public class LoginQueryValidatorXUnitTests
{
    private readonly LoginQueryValidator validator;

    public LoginQueryValidatorXUnitTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenEmailIsEmptyAsync()
    {
        //Arrange
        var request = new LoginQuery("", "password");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenEmailIsNotWellFormedAsync()
    {
        //Arrange
        var request = new LoginQuery("adsd", "password");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPasswordIsEmptyAsync()
    {
        //Arrange
        var request = new LoginQuery("user@test.com", "");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPasswordAndEmailAreEmptyAsync()
    {
        //Arrange
        var request = new LoginQuery("", "");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(3, result.Errors.Count);
    }

    [Fact]
    public void Validator_Should_ValidateWithoutErrors_WhenRequestIsCompleted()
    {
        //Arrange
        var request = new LoginQuery("user@test.com", "password");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
        Assert.Equal(0, result.Errors.Count);
    }
}
