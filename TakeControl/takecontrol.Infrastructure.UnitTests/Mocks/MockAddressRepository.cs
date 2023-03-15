using AutoFixture;
using Takecontrol.Domain.Models.Addresses;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Identity;

namespace Takecontrol.Infrastructure.IntegrationTests.Mocks;

public static class MockAddressRepository
{
    public static Guid AddressIdTest = new Guid("2a1b1095-4c7a-40d2-a9f4-cd6bb718da95");

    public static async Task AddDataAddressRepository(TakeControlDbContext TakecontrolDbContextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var addresses = fixture.CreateMany<Address>().ToList();
        addresses.Add(fixture.Build<Address>()
            .Without(c => c.Club)
            .With(c => c.Id, AddressIdTest)
            .Create()
            );

        TakecontrolDbContextFake.Addresses!.AddRange(addresses);
        await TakecontrolDbContextFake.SaveChangesAsync();
    }
}
