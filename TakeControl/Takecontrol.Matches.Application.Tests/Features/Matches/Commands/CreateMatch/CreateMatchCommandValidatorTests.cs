using Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;
using Takecontrol.Shared.Tests.Constants;

namespace Takecontrol.Matches.Application.Tests.Features.Matches.Commands.CreateMatch;

[Trait("Category", Category.UnitTest)]
public class CreateMatchCommandValidatorTests
{
    private readonly CreateMatchCommandValidator _validator;

    public CreateMatchCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateThePlayerId(string id, bool isValid)
    {
        //Arrange
        var playerId = new Guid(id);
        var reservationId = Guid.NewGuid();
        var command = new CreateMatchCommand(playerId, reservationId);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateTheReservationId(string id, bool isValid)
    {
        //Arrange
        var reservationId = new Guid(id);
        var playerId = Guid.NewGuid();
        var command = new CreateMatchCommand(playerId, reservationId);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
