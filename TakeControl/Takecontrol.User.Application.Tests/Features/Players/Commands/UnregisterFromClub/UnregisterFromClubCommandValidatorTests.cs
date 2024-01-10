using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Features.Players.Commands.UnregisterFromClub;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.UnregisterFromClub;

[Trait("Category", Category.UnitTest)]
public class UnregisterFromClubCommandValidatorTests
{
    private readonly UnregisterFromClubCommandValidator _validator;

    public UnregisterFromClubCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateTheUserId(string userId, bool isValid)
    {
        //Arrange
        var clubId = Guid.NewGuid();
        var command = new UnregisterFromClubCommand(new Guid(userId), clubId);

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_ValidateTheClubId(string clubId, bool isValid)
    {
        //Arrange
        var userId = Guid.NewGuid();
        var command = new UnregisterFromClubCommand(userId, new Guid(clubId));

        //Act
        var result = _validator.Validate(command);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
