using MediatR;
using System.Windows.Input;
using takecontrol.Application.Abstractions.Mediatr;

namespace takecontrol.Application.Features.Clubs.Commands.RegisterClub;

public class RegisterClubCommand : ICommand<Unit>
{
    public string Name { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Province { get; private set; } = string.Empty;
    public string MainAddress { get; private set; } = string.Empty;

    public RegisterClubCommand(string? name, string? city, string? province, string? mainAddress)
    {
        Name = name;
        City = city;
        Province = province;
        MainAddress = mainAddress;
    }
}
