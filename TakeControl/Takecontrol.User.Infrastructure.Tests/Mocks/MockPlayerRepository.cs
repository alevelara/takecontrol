using AutoFixture;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Takecontrol.User.Domain.Models.Players;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.User.Infrastructure.Tests.Mocks;

public static class MockPlayerRepository
{
    public static async Task AddPlayers(TakeControlDbContext TakecontrolDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var players = fixture.CreateMany<Player>().ToList();
        players.Add(fixture.Build<Player>()
            .With(c => c.UserId, Guid.NewGuid())
            .Create()
            );

        TakecontrolDbContextFake.Players!.AddRange(players);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerWithUserId(TakeControlDbContext TakecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name", 2, 3, 5);
        TakecontrolDbContextFake.Players!.Add(player);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerBeginner(TakeControlDbContext TakecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name beginner", 2, 1, 1);

        TakecontrolDbContextFake.Players!.Add(player);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerMid(TakeControlDbContext TakecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name middle", 2, 3, 3);

        TakecontrolDbContextFake.Players!.Add(player);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddPlayerExpert(TakeControlDbContext TakecontrolDbContextFake, Guid userId)
    {
        var player = Player.Create(userId, "name expert", 2, 3, 9);

        TakecontrolDbContextFake.Players!.Add(player);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AssignPlayerToClub(TakeControlDbContext TakecontrolDbContextFake, Guid clubId, Guid playerId)
    {
        var playerGuid = TakecontrolDbContextFake.Players.Where(x => x.UserId == playerId).First();
        var clubGuid = TakecontrolDbContextFake.Clubs.Where(x => x.UserId == clubId).First();

        var playerClub = PlayerClub.Create(playerGuid.Id, clubGuid.Id);

        TakecontrolDbContextFake.PlayerClubs!.Add(playerClub);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }
}
