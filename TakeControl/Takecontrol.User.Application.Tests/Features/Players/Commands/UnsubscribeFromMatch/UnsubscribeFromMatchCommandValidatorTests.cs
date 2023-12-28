using FluentValidation;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Features.Players.Commands.JoinToClub;
using Takecontrol.User.Application.Features.Players.Commands.UnsubscribeFromMatch;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.UnsubscribeFromMatch;

[Trait("Category", Category.UnitTest)]
public class UnsubscribeFromMatchCommandValidatorTests
{
    private readonly UnsubscribeFromMatchCommandValidator _validator;

    public UnsubscribeFromMatchCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateTheUserId(string id, bool isValid)
    {
        //Arrange
        var playerId = new Guid(id);
        var matchId = Guid.NewGuid();
        var command = new UnsubscribeFromMatchCommand(playerId, matchId);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateTheMatchId(string id, bool isValid)
    {
        //Arrange
        var matchId = new Guid(id);
        var playerId = Guid.NewGuid();
        var command = new UnsubscribeFromMatchCommand(playerId, matchId);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
