﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using takecontrol.Application.Constants;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Exceptions;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Mappings.Identity;
using takecontrol.Domain.Models.ApplicationUser.Options;
using takecontrol.Identity.Models;

namespace takecontrol.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> Login(LoginQuery request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        ValidateUser(user, request.Email);

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        if (!signInResult.Succeeded)
            throw new UnauthorizedException("Credentials are wrong.");

        var token = await GenerateToken(user);
        var authResponse = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email,
            UserName = user.UserName, 
            UserType = user.UserType
        };

        return authResponse;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(CustomClaimsTypes.Uid, user.Id.ToString())
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signInCredentials);

        return jwtSecurityToken;
    }

    private void ValidateUser(ApplicationUser user, string userEmail)
    {
        if (user == null)
            throw new ConflictException($"User with email {userEmail} doesn't exists.");
        
        if (String.IsNullOrEmpty(user.Email))
            throw new ConflictException($"An error occurred during the registration proccess: Email of user with email {userEmail} doesn't exists.");

        if (String.IsNullOrEmpty(user.UserName))
            throw new ConflictException($"An error occurred during the registration proccess: UserName of user with email {userEmail} doesn't exists");        
    }
}
