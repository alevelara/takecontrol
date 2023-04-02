using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.User.Application.Features.Players.Commands.RegisterPlayer;

public class RegisterPlayerCommand : ICommand<Unit>
{
    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public int NumberOfClassesInAWeek { get; private set; }

    public int AvgNumberOfMatchesInAWeek { get; private set; }

    public int NumberOfYearsPlayed { get; private set; }

    public RegisterPlayerCommand(string name, string email, string password, int numberOfClassesInAWeek, int avgNumberOfMatchesInAWeek, int numberOfYearsPlayed)
    {
        Name = name;
        Email = email;
        Password = password;
        NumberOfClassesInAWeek = numberOfClassesInAWeek;
        AvgNumberOfMatchesInAWeek = avgNumberOfMatchesInAWeek;
        NumberOfYearsPlayed = numberOfYearsPlayed;
    }
}
