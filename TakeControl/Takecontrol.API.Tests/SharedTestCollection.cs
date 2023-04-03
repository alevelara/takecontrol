using Takecontrol.API.Tests.Primitives;
using Xunit;

namespace Takecontrol.API.Tests;

[CollectionDefinition(Name)]
public class SharedTestCollection : ICollectionFixture<ApiWebApplicationFactory<Program>>
{
    public const string Name = "IntegrationTests";
}
