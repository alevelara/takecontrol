using AutoFixture;
using Castle.DynamicProxy.Generators;
using Takecontrol.Domain.Models.Addresses;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Identity;

namespace Takecontrol.Infrastructure.IntegrationTests.Mocks;

public static class MockClubRepository
{
    public static async Task AddClubs(TakeControlDbContext takecontrolDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var clubs = fixture.CreateMany<Club>().ToList();
        clubs.Add(fixture.Build<Club>()
            .With(c => c.UserId, Guid.NewGuid())
            .Without(c => c.Address)
            .Create()
            );

        takecontrolDbContextFake.Clubs!.AddRange(clubs);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddClubsWithAddress(TakeControlDbContext takecontrolDbContextFake)
    {
        var clubs = new List<Club>();
        var addresses = new List<Address>();

        for (var i = 0; i < 10; i++)
        {
            var address = Address.Create("city", "province", "mainaddress");
            addresses.Add(address);
            clubs.Add(Club.Create(address.Id, Guid.NewGuid(), "name"));
        }

        takecontrolDbContextFake.Addresses!.AddRange(addresses);
        takecontrolDbContextFake.Clubs!.AddRange(clubs);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task AddClubWithUserId(TakeControlDbContext takecontrolDbContextFake, Guid userId)
    {
        var address = Address.Create("city", "province", "mainaddress");
        var club = Club.Create(address.Id, userId, "name");

        takecontrolDbContextFake.Addresses!.Add(address);
        takecontrolDbContextFake.Clubs!.Add(club);
        await takecontrolDbContextFake.SaveChangesAsync();
    }

    public static async Task<Club> AddClub(TakeControlDbContext takecontrolDbContextFake)
    {
        var address = Address.Create("city", "province", "mainaddress");
        var club = Club.Create(address.Id, Guid.NewGuid(), "name");

        takecontrolDbContextFake.Addresses!.Add(address);
        takecontrolDbContextFake.Clubs!.Add(club);
        await takecontrolDbContextFake.SaveChangesAsync();
        return club;
    }
}
