using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Shared.Tests.Constants;

namespace Takecontrol.Matches.Domain.Tests.Models.Courts;

[Trait("Category", Category.UnitTest)]
public class CourtXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewCourt_WhenAllFieldsArePopulated()
    {
        var clubId = Guid.NewGuid();
        var name = "name";

        var court = Court.Create(clubId, name);

        Assert.NotNull(court);
        Assert.NotEqual(Guid.Empty, court.Id);
        Assert.Equal(name, court.Name);
        Assert.Equal(clubId, court.ClubId);
    }

    [Fact]
    public void Create_Should_ReturnNewCourt_WhenNameIsEmpty()
    {
        var clubId = Guid.NewGuid();
        var name = string.Empty;

        var court = Court.Create(clubId, name);

        Assert.NotNull(court);
        Assert.NotEqual(Guid.Empty, court.Id);
        Assert.Equal(name, court.Name);
        Assert.Equal(clubId, court.ClubId);
    }

    [Fact]
    public void Create_Should_ReturnNewCourt_WhenClubIdIsEmpty()
    {
        var clubId = Guid.Empty;
        var name = "name";

        var court = Court.Create(clubId, name);

        Assert.NotNull(court);
        Assert.NotEqual(Guid.Empty, court.Id);
        Assert.Equal(name, court.Name);
        Assert.Equal(clubId, court.ClubId);
    }
}
