﻿using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.Application.Features.Accounts.Queries.Login;

public sealed class LoginQueryHandler : IQueryHandler<LoginQuery, AuthResponse>
{
    private readonly IAuthService _authService;

    public LoginQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request);
    }
}
