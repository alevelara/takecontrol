using Takecontrol.Application.Features.Players.Commands.RegisterPlayer;

namespace Takecontrol.Application.Tests.Features.Players.Commands.RegisterPlayer;

[Trait("Category", "UnitTests")]
public class RegisterPlayerCommandValidatorXUnitTests
{
    private readonly RegisterPlayerCommandValidator validator;

    public RegisterPlayerCommandValidatorXUnitTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenNameIsNull()
    {
        //Arrange
        var request = new RegisterPlayerCommand(null, "email@test.com", "Password123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenNameIsEmpty()
    {
        //Arrange
        var request = new RegisterPlayerCommand("", "email@test.com", "Password123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenCityIsNull()
    {
        //Arrange
        var request = new RegisterPlayerCommand(null, "email@test.com", "Password123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenEmailIsInvalid()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email", "Password123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenEmailIsEmpty()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "", "Password123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenEmailIsNull()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", null, "Password123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordIsEmpty()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(6, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordIsNull()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", null, 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(6, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordLengthIsHigherThan14chars()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "P@sswordHigherThan14", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordLengthIsSmallerThan8chars()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "!8Char", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordLengthHasNotAnyLowercase()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "MAYUS123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordLengthHasNotAnyUppercase()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "lower123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordLengthHasNotAnyDigit()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "AnyDigit!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenPaswordLengthHasNotAnySymbol()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "Anysymbol123", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenNumberOfClassesInAWeekIsNegative()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "Password123!", -1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenAvgNumberOfMatchesInAWeekIsNegative()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, -1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenAvgNumberOfYearsPlayedIsNegative()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, 1, -1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnOk_WhenCommandIsValid()
    {
        //Arrange
        var request = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, 1, 1);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
}
