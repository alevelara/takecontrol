namespace takecontrol.Domain.Messages.Clubs;

public class RegisterClubRequest
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string MainAddress { get; set; } = string.Empty;
}
