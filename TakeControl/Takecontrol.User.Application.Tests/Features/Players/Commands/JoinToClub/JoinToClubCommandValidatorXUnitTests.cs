using Takecontrol.User.Application.Features.Players.Commands.JoinToClub;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.JoinToClub;

[Trait("Category", "UnitTests")]
public class JoinToClubCommandValidatorXUnitTests : IClassFixture<JoinToClubCommandValidator>
{
    private readonly JoinToClubCommandValidator _validator;

    public JoinToClubCommandValidatorXUnitTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("12345", true)]
    [InlineData("123", false)]
    [InlineData("123456", false)]
    [InlineData("", false)]
    public void Validator_Should_ValidateTheCode(string code, bool isValid)
    {
        //Arrange
        var playerId = Guid.NewGuid();
        var clubId = Guid.NewGuid();
        var command = new JoinToClubCommand(playerId, clubId, code);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateThePlayerId(string id, bool isValid)
    {
        //Arrange
        var playerId = new Guid(id);
        var clubId = Guid.NewGuid();
        var code = "12345";
        var command = new JoinToClubCommand(playerId, clubId, code);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateTheClubId(string id, bool isValid)
    {
        //Arrange
        var clubId = new Guid(id);
        var playerId = Guid.NewGuid();
        var code = "12345";
        var command = new JoinToClubCommand(playerId, clubId, code);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
