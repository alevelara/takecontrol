using System.Net.Http.Json;
using Takecontrol.User.Domain.Messages.Players.Requests;

namespace Takecontrol.API.Tests.Controllers.TestData;

public static class PlayerTestData
{
    private const string RegisterPlayerEndpoint = "api/v1/player/Register";

    public static async Task RegisterPlayerForTest(HttpClient httpClient)
    {
        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: 1);

        await httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);
    }
}
