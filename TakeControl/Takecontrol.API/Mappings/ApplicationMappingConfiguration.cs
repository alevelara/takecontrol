﻿using Mapster;
using Takecontrol.Credential.Application.Features.Accounts.Commands.ResetPassword;
using Takecontrol.Credential.Application.Features.Accounts.Commands.UpdatePassword;
using Takecontrol.Credential.Application.Features.Accounts.Queries.Login;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;
using Takecontrol.User.Application.Features.Players.Commands.JoinToClub;
using Takecontrol.User.Application.Features.Players.Commands.RegisterPlayer;
using Takecontrol.User.Domain.Dtos.Addresses;
using Takecontrol.User.Domain.Dtos.Clubs;
using Takecontrol.User.Domain.Dtos.Players;
using Takecontrol.User.Domain.Messages.Clubs;
using Takecontrol.User.Domain.Messages.Players;
using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.API.Mappings
{
    public class ApplicationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            this.AddNewConfigForClubs(config);
            this.AddNewConfigForAuthentication(config);
            this.AddNewConfigForPlayers(config);
        }

        private void AddNewConfigForAuthentication(TypeAdapterConfig config)
        {
            config.NewConfig<AuthRequest, LoginQuery>();
            config.NewConfig<ResetPasswordRequest, ResetPasswordCommand>();
            config.NewConfig<RegisterClubRequest, RegisterClubCommand>();
            config.NewConfig<RegisterPlayerRequest, RegisterPlayerCommand>();
            config.NewConfig<UpdatePasswordRequest, UpdatePasswordCommand>();
        }

        private void AddNewConfigForClubs(TypeAdapterConfig config)
        {
            config.NewConfig<Address, AddressDto>();
            config.NewConfig<Club, ClubDto>()
                .Map(dest => dest.Address, src => src.Address);
            config.NewConfig<Club, RestrictedClubDto>()
                .Map(dest => dest.Address, src => src.Address);
        }

        private void AddNewConfigForPlayers(TypeAdapterConfig config)
        {
            config.NewConfig<Player, PlayerDto>()
                .Map(dest => dest.PlayerLevel, src => src.PlayerLevel);
            config.NewConfig<JoinToClubRequest, JoinToClubCommand>();
        }
    }
}