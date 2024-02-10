using Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;
using Takecontrol.User.Application.Features.Players.Commands.CancelMatch;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.CancelMatch;

public class CancelMatchCommandValidatorTests
{
    private readonly CancelMatchCommandValidator _validator;

    public CancelMatchCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateTheUserId(string id, bool isValid)
    {
        //Arrange
        var userId = new Guid(id);
        var matchId = Guid.NewGuid();
        var command = new CancelMatchCommand(userId, matchId);

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
        var userId = Guid.NewGuid();
        var command = new CancelMatchCommand(userId, matchId);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }

}
