using AutoFixture;
using Castle.DynamicProxy.Generators;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.IntegrationTests.Mocks;

public static class MockClubRepository
{
    public static Guid addressIdTest = new Guid("2a1b1095-4c7a-40d2-a9f4-cd6bb718da95");

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
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var clubs = fixture.CreateMany<Club>().ToList();

        clubs.Add(fixture.Build<Club>()
            .With(c => c.UserId, Guid.NewGuid())
            .With(c => c.Address, fixture.Create<Address>())
            .Create()
            );

        takecontrolDbContextFake.Clubs!.AddRange(clubs);
        await takecontrolDbContextFake.SaveChangesAsync();
    }
}
