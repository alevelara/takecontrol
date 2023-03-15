using Takecontrol.Application.Features.Clubs.Commands.RegisterClub;

namespace Takecontrol.Application.Tests.Features.Clubs.Commands.RegisterClub;

[Trait("Category", "UnitTests")]
public class RegisterClubCommandValidatorXUnitTests
{
    private readonly RegisterClubCommandValidator validator;

    public RegisterClubCommandValidatorXUnitTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenNameIsNull()
    {
        //Arrange
        var request = new RegisterClubCommand(null, "city", "province", "mainAddress", "email@test.com", "Password123!");

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
        var request = new RegisterClubCommand("", "city", "province", "mainAddress", "email@test.com", "Password123!");

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
        var request = new RegisterClubCommand("name", null, "province", "mainAddress", "email@test.com", "Password123!");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenCityIsEmpty()
    {
        //Arrange
        var request = new RegisterClubCommand("name", "", "province", "mainAddress", "email@test.com", "Password123!");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenProvinceIsNull()
    {
        //Arrange
        var request = new RegisterClubCommand("name", "city", null, "mainaddress", "email@test.com", "Password123!");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenProvinceIsEmpty()
    {
        //Arrange
        var request = new RegisterClubCommand("name", "city", "", "mainaddress", "email@test.com", "Password123!");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenMainAddressIsNull()
    {
        //Arrange
        var request = new RegisterClubCommand("name", "city", "province", null, "email@test.com", "Password123!");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenMainAddressIsEmpty()
    {
        //Arrange
        var request = new RegisterClubCommand("name", "city", "province", "", "email@test.com", "Password123!");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenEmailIsInvalid()
    {
        //Arrange
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email", "Password123!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "", "Password123!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", null, "Password123!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", null);

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password12345678!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Pas1!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "PASSWORD123!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "password123!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password!");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password123");

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password123!");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
}
