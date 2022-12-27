using takecontrol.API.IntegrationTests.Primitives;

namespace takecontrol.API.IntegrationTests;

[CollectionDefinition("IntegrationTests")]
public class SharedTestCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
{
}
