using System.Net.Http.Json;
using Takecontrol.API.Tests.Helpers;
using Takecontrol.User.Domain.Messages.Players.Requests;

namespace Takecontrol.API.Tests.Controllers.TestData;

public static class PlayerTestData
{
    public static async Task RegisterPlayerForTest(HttpClient httpClient)
    {
        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: 1);

        await httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);
    }

    public static async Task JoinAPlayerToClub(HttpClient httpClient, Guid userId, Guid clubId, string code)
    {
        var request = new JoinToClubRequest(userId, clubId, code);

        await httpClient.PostAsJsonAsync(Endpoints.JoinToClub, request, default);

    }
}
