using Takecontrol.Shared.Tests.MockContexts;

namespace Takecontrol.Shared.Tests.Factories;

public static class DbContextFactory
{
    public static TakeControlDb CreateTakeControlDbContext() => new TakeControlDb();
    public static TakeControlIdentityDb CreateTakeControlIdentityDbContext() => new TakeControlIdentityDb();
    public static TakeControlEmailDb CreateTakeControlEmailDbContext() => new TakeControlEmailDb();
    public static TakeControlMatchesDb CreateTakeControlMatchDbContext() => new TakeControlMatchesDb();
}
