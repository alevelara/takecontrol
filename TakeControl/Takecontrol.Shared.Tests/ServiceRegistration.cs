using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Shared.Tests.Contracts.Clubs;
using Takecontrol.Shared.Tests.Contracts.Courts;
using Takecontrol.Shared.Tests.Contracts.Matches;
using Takecontrol.Shared.Tests.Contracts.Players;
using Takecontrol.Shared.Tests.Contracts.Reservations;
using Takecontrol.Shared.Tests.Repositories.Clubs;
using Takecontrol.Shared.Tests.Repositories.Courts;
using Takecontrol.Shared.Tests.Repositories.Matches;
using Takecontrol.Shared.Tests.Repositories.Players;
using Takecontrol.Shared.Tests.Repositories.Reservations;

namespace Takecontrol.Shared.Tests;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureSharedTestInfrastructureServices(this IServiceCollection service)
    {
        service.AddScoped<ITestReservationReadRepository, TestReservationReadRepository>();
        service.AddScoped<ITestReservationWriteRepository, TestReservationWriteRepository>();
        service.AddTransient<ITestMatchReadRepository, TestMatchReadRepository>();
        service.AddScoped<ITestMatchWriteRepository, TestMatchWriteRepository>();
        service.AddScoped<ITestCourtReadRepository, TestCourtReadRepository>();
        service.AddScoped<ITestPlayerReadRepository, TestPlayerReadRepository>();
        service.AddScoped<ITestClubReadRepository, TestClubReadRepository>();

        return service;
    }
}