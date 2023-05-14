using MediatR;
using Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;
using Takecontrol.Matches.Infrastructure.Tests.Mocks;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Infrastructure.Tests.Features.Courts.Commands.RegisterCourtsByClub;

[Trait("Category", SharedTestCollection.Name)]
public class RegisterCourtsByClubCommandHandlerXUnitTests
{
    private readonly MockUnitOfWork _uoW;

    public RegisterCourtsByClubCommandHandlerXUnitTests()
    {
        _uoW = new MockUnitOfWork();
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(2)]
    [InlineData(0)]
    public async Task Handle_Should_CreateSameNumberOfCourts_WhenIsPopulated(int numberOfCourts)
    {
        var clubId = Guid.NewGuid();
        var command = new RegisterCourtsByClubCommand(clubId, numberOfCourts, new TimeOnly(10, 0), new TimeOnly(12, 0));
        var handler = new RegisterCourtsByClubCommandHandler(_uoW.GetUnitOfWork().Object);
        var context = _uoW.GetContext();

        var result = await handler.Handle(command, default);

        var courts = context.Courts.Where(c => c.ClubId == clubId).ToList();

        Assert.IsType<Unit>(result);
        Assert.Equal(numberOfCourts, courts.Count);
    }
}
