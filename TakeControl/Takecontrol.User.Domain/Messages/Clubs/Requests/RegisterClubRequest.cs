namespace Takecontrol.User.Domain.Messages.Clubs.Requests;

public sealed record class RegisterClubRequest(
    string Name,
    string City,
    string Province,
    string MainAddress,
    string Email,
    string Password
    );