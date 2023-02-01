using Mapster;
using takecontrol.Application.Features.Accounts.Commands.ResetPassword;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using takecontrol.Domain.Dtos.Clubs;
using takecontrol.Domain.Messages.Clubs;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Messages.Players;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.API.Mappings
{
    public class ApplicationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Club, ClubDto>();
            config.NewConfig<AuthRequest, LoginQuery>();
            config.NewConfig<ResetPasswordRequest, ResetPasswordCommand>();
            config.NewConfig<RegisterClubRequest, RegisterClubCommand>();
            config.NewConfig<RegisterPlayerRequest, RegisterPlayerCommand>();
        }
    }
}
