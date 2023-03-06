using takecontrol.Application.Features.Clubs.Queries.GetByUserId;
using takecontrol.Application.Features.Clubs.Queries.GetClubById;

namespace takecontrol.Application.UnitTests.Features.Clubs.Queries.GetClubByUserId;

public class GetClubByUserIdQueryValidatorXUnitTests : IClassFixture<GetClubByUserIdQueryValidator>
{
    private readonly GetClubByUserIdQueryValidator _validator;

    public GetClubByUserIdQueryValidatorXUnitTests()
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
        var query = new GetClubByUserIdQuery(userId);

        //Act
        var result = _validator.Validate(query);

        //Assert
        Assert.Equal(isValid, result.IsValid);
    }
}
