using System.Net.Http.Json;
using Takecontrol.User.Domain.Messages.Clubs.Requests;

namespace Takecontrol.API.Tests.Controllers.TestData;

public static class ClubTestData
{
    private const string RegisterClubEndpoint = "api/v1/club/Register";

    public static async Task RegisterClubForTest(HttpClient httpClient)
    {
        var request = new RegisterClubRequest(
            Email: "club@test.com",
            Name: "nameTest",
            Password: "Password123!",
            City: "City",
            MainAddress: "mainAddress",
            Province: "province",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00"));

        await httpClient.PostAsJsonAsync(RegisterClubEndpoint, request, default);
    }
}
