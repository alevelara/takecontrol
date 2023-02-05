﻿using Mapster;
using takecontrol.Application.Features.Accounts.Commands.ResetPassword;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using takecontrol.Domain.Dtos.Addresses;
using takecontrol.Domain.Dtos.Clubs;
using takecontrol.Domain.Messages.Clubs;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Messages.Players;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.API.Mappings
{
    public class ApplicationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            this.AddNewConfigForClubs(config);
            this.AddNewConfigForAuthentication(config);
        }

        private void AddNewConfigForAuthentication(TypeAdapterConfig config)
        {
            config.NewConfig<AuthRequest, LoginQuery>();
            config.NewConfig<ResetPasswordRequest, ResetPasswordCommand>();
            config.NewConfig<RegisterClubRequest, RegisterClubCommand>();
            config.NewConfig<RegisterPlayerRequest, RegisterPlayerCommand>();
        }

        private void AddNewConfigForClubs(TypeAdapterConfig config)
        {
            config.NewConfig<Address, AddressDto>();
            config.NewConfig<Club, ClubDto>()
                .Map(dest => dest.Address, src => src.Address);
        }
    }
}