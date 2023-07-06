using AutoFixture;
using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.User.Infrastructure.Tests.Mocks;

public static class MockClubRepository
{
    public static async Task AddClubs(TakeControlDbContext TakecontrolDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var clubs = fixture.CreateMany<Club>().ToList();
        clubs.Add(fixture.Build<Club>()
            .With(c => c.UserId, Guid.NewGuid())
            .Without(c => c.Address)
            .Create()
            );

        await TakecontrolDbContextFake.Clubs!.AddRangeAsync(clubs);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddClubsWithAddress(TakeControlDbContext TakecontrolDbContextFake)
    {
        var clubs = new List<Club>();
        var addresses = new List<Address>();

        for (var i = 0; i < 10; i++)
        {
            var address = Address.Create("city", "province", "mainaddress");
            addresses.Add(address);
            clubs.Add(Club.Create(address.Id, Guid.NewGuid(), "name", 1, new TimeOnly(10, 0), new TimeOnly(12, 0)));
        }

        await TakecontrolDbContextFake.Addresses!.AddRangeAsync(addresses);
        await TakecontrolDbContextFake.Clubs!.AddRangeAsync(clubs);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task<Guid> AddClubWithUserId(TakeControlDbContext TakecontrolDbContextFake, Guid userId)
    {
        var address = Address.Create("city", "province", "mainaddress");
        var club = Club.Create(address.Id, userId, "name", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

        await TakecontrolDbContextFake.Addresses!.AddAsync(address);
        await TakecontrolDbContextFake.Clubs!.AddAsync(club);
        await TakecontrolDbContextFake.SaveChangesAsync();

        return club.Id;
    }

    public static async Task<Club> AddClub(TakeControlDbContext TakecontrolDbContextFake)
    {
        var address = Address.Create("city", "province", "mainaddress");
        var club = Club.Create(address.Id, Guid.NewGuid(), "name", 1, new TimeOnly(10, 0), new TimeOnly(12, 0));

        await TakecontrolDbContextFake.Addresses!.AddAsync(address);
        await TakecontrolDbContextFake.Clubs!.AddAsync(club);
        await TakecontrolDbContextFake.SaveChangesAsync();
        return club;
    }
}
