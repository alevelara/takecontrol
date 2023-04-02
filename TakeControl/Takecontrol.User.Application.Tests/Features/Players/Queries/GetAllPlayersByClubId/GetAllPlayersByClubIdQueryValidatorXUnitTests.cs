using Takecontrol.User.Application.Features.Players.Queries.GetAllPlayersByClubId;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Queries.GetAllPlayersByClubId;

public class GetAllPlayersByClubIdQueryValidatorXUnitTests : IClassFixture<GetAllPlayersByClubIdQueryValidator>
{
    private readonly GetAllPlayersByClubIdQueryValidator _validator;

    public GetAllPlayersByClubIdQueryValidatorXUnitTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("3f365c64-808e-496d-a818-9c649c663f01", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_Validate(string guid, bool isValid)
    {
        //Arrange
        var clubId = new Guid(guid);
        var query = new GetAllPlayersByClubIdQuery(clubId);

        //Act
        var result = _validator.Validate(query);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }

    [Theory]
    [InlineData(null, false)]
    public void Validator_Should_ReturnInvalidValue_WhenGuidIsNull(Guid? guid, bool isValid)
    {
        //Arrange
        var query = new GetAllPlayersByClubIdQuery(guid);

        //Act
        var result = _validator.Validate(query);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
