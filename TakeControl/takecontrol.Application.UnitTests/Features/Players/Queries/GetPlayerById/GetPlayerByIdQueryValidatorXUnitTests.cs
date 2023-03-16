using takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;

namespace takecontrol.Application.UnitTests.Features.Players.Queries.GetPlayerByUserId;

public class GetPlayerByIdQueryValidatorXUnitTests : IClassFixture<GetPlayerByIdQueryValidator>
{
    private readonly GetPlayerByIdQueryValidator _validator;

    public GetPlayerByIdQueryValidatorXUnitTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("3f365c64-808e-496d-a818-9c649c663f01", true)]
    [InlineData("00000000-0000-0000-0000-000000000000", false)]
    public void Validator_Should_Validate(string guid, bool isValid)
    {
        //Arrange
        var userId = new Guid(guid);
        var query = new GetPlayerByIdQuery(userId);

        //Act
        var result = _validator.Validate(query);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
