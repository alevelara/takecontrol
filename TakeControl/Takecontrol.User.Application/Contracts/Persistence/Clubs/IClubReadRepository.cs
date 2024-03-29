﻿using Takecontrol.User.Domain.Models.Clubs;

namespace Takecontrol.User.Application.Contracts.Persistence.Clubs;

public interface IClubReadRepository
{
    Task<Club?> GetClubByUserId(Guid userId);

    Task<List<Club>> GetAllClubsAsync();

    Task<Club?> GetClubByCodeAndUserId(Guid clubId, string code);
}
