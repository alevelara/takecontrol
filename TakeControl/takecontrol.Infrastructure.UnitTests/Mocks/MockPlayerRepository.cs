using AutoFixture;
using Castle.DynamicProxy.Generators;
using takecontrol.Domain.Models.PlayerClubs;
using takecontrol.Domain.Models.Players;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.IntegrationTests.Mocks;

public static class MockPlayerRepository
{
    public static async Task AddPlayers(TakeControlDbContext takecontrolDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var players = fixture.CreateMany<Player>().ToList();
        players.Add(fixture.Build<Player>()
            .With(c => c.UserId, Guid.NewGuid())
            .Create()
            );

        takecontrolDbContextFake.Players!.AddRange(players);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerWithUserId(TakeControlDbContext takecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name", 2, 3, 5);
        takecontrolDbContextFake.Players!.Add(player);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerBeginner(TakeControlDbContext takecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name beginner", 2, 1, 1);

        takecontrolDbContextFake.Players!.Add(player);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerMid(TakeControlDbContext takecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name middle", 2, 3, 3);

        takecontrolDbContextFake.Players!.Add(player);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerExpert(TakeControlDbContext takecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name expert", 2, 3, 9);

        takecontrolDbContextFake.Players!.Add(player);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AssignPlayerToClub(TakeControlDbContext takecontrolDbContextFake, Guid clubId, Guid playerId)
    {
        var playerGuid = takecontrolDbContextFake.Players.Where(x => x.UserId == playerId).First();
        var clubGuid = takecontrolDbContextFake.Clubs.Where(x => x.UserId == clubId).First();

        var playerClub = PlayerClub.Create(playerGuid.Id, clubGuid.Id);

        takecontrolDbContextFake.PlayerClubs!.Add(playerClub);
        await takecontrolDbContextFake.SaveChangesAsync();
    }
}
