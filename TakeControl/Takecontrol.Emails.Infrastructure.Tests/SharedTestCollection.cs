using Takecontrol.Shared.Tests.MockContexts;
using Xunit;

namespace Takecontrol.Emails.Infrastructure.Tests;

[CollectionDefinition(Name)]
public class SharedTestCollection : ICollectionFixture<TakeControlEmailDb>
{
    public const string Name = "EmailIntegrationTests";
}
