using AutoFixture;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.IntegrationTests.Mocks;

public static class MockAddressRepository
{
    public static Guid AddressIdTest = new Guid("2a1b1095-4c7a-40d2-a9f4-cd6bb718da95");

    public static async Task AddDataAddressRepository(TakeControlDbContext takecontrolDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var addresses = fixture.CreateMany<Address>().ToList();
        addresses.Add(fixture.Build<Address>()
            .Without(c => c.Club)
            .With(c => c.Id, AddressIdTest)
            .Create()
            );

        takecontrolDbContextFake.Addresses!.AddRange(addresses);
        await takecontrolDbContextFake.SaveChangesAsync();
    }
}
