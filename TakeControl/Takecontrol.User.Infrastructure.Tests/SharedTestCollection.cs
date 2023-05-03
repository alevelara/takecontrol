using Takecontrol.Shared.Tests.MockContexts;
using Xunit;

namespace Takecontrol.User.Infrastructure.Tests;

[CollectionDefinition(Name)]
public class SharedTestCollection : ICollectionFixture<TakeControlDb>
{
    public const string Name = "InfrastructureIntegrationTests";
}
