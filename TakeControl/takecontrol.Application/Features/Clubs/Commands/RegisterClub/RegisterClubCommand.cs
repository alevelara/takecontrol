using System.Windows.Input;
using MediatR;
using takecontrol.Application.Abstractions.Mediatr;

namespace takecontrol.Application.Features.Clubs.Commands.RegisterClub;

public class RegisterClubCommand : ICommand<Unit>
{
    public string Name { get; private set; }
    public string City { get; private set; }
    public string Province { get; private set; }
    public string MainAddress { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public RegisterClubCommand(string? name, string? city, string? province, string? mainAddress, string email, string password)
    {
        Name = name;
        City = city;
        Province = province;
        MainAddress = mainAddress;
        Email = email;
        Password = password;
    }
}
