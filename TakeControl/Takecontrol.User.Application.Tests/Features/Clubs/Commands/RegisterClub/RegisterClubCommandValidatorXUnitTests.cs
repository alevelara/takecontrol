﻿using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Clubs.Commands.RegisterClub;

[Trait("Category", Category.UnitTest)]
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
        var request = new RegisterClubCommand(null, "city", "province", "mainAddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand(string.Empty, "city", "province", "mainAddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", null, "province", "mainAddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", string.Empty, "province", "mainAddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", null, "mainaddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", string.Empty, "mainaddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", null, "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", string.Empty, "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", string.Empty, "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", null, "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", string.Empty, 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", null, 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password12345678!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Pas1!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "PASSWORD123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password123", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

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
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnError_WhenTimeIsValid()
    {
        //Arrange
        var request = new RegisterClubCommand("name", "city", "province", "mainAddress", "email@test.com", "Password123!", 1, new TimeOnly(10, 0), new TimeOnly(11, 0));

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.False(result.IsValid);
    }
}
