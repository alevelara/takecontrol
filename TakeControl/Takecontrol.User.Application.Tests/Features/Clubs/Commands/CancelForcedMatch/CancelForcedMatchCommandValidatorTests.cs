using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Features.Clubs.Commands.CancelForcedMatch;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Clubs.Commands.CancelForcedMatch;

[Trait("Category", Category.UnitTest)]
public class CancelForcedMatchCommandValidatorTests
{
    private readonly CancelForcedMatchCommandValidator validator;

    public CancelForcedMatchCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenClubIdIsEmpty()
    {
        //Arrange
        var request = new CancelForcedMatchCommand(Guid.Empty, Guid.NewGuid(), "description");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenMatchIdIsEmpty()
    {
        //Arrange
        var request = new CancelForcedMatchCommand(Guid.NewGuid(), Guid.Empty, "description");

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenDescriptionIsEmpty()
    {
        //Arrange
        var request = new CancelForcedMatchCommand(Guid.NewGuid(), Guid.NewGuid(), string.Empty);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Single(result.Errors);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnValidationException_WhenDescriptionIsNull()
    {
        //Arrange
        var request = new CancelForcedMatchCommand(Guid.NewGuid(), Guid.NewGuid(), null);

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.Equal(2, result.Errors.Count);
        Assert.False(result.IsValid);
    }
}
