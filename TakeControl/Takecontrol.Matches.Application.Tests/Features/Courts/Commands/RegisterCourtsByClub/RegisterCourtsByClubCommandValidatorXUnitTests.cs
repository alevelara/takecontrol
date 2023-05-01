using Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Tests.Features.Courts.Commands.RegisterCourtsByClub;

public class RegisterCourtsByClubCommandValidatorXUnitTests : IClassFixture<RegisterCourtsByClubCommandValidator>
{
    private readonly RegisterCourtsByClubCommandValidator _validator;

    public RegisterCourtsByClubCommandValidatorXUnitTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("3a0a3ad1-acc8-45eb-8ae8-364b6e805274", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_Validate(string guid, bool isValid)
    {
        //Arrange
        var userId = new Guid(guid);
        var query = new RegisterCourtsByClubCommand(userId, 3);

        //Act
        var result = _validator.Validate(query);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
