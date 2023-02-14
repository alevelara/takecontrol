using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using takecontrol.Application.Constants;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Exceptions;
using takecontrol.Application.Features.Accounts.Commands.ResetPassword;
using takecontrol.Application.Features.Accounts.Commands.UpdatePassword;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.ApplicationUser.Options;
using takecontrol.Identity.Constants;
using takecontrol.Identity.Models;
using IdentityError = takecontrol.Domain.Errors.Identity.IdentityError;

namespace takecontrol.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }

    public async Task<AuthResponse> Login(LoginQuery request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        ValidateUser(user, request.Email);

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        if (!signInResult.Succeeded)
            throw new UnauthorizedException(IdentityError.InvalidCredentials);

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

    public async Task<Guid> Register(RegistrationRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Email);
        if (existingUser != null)
            throw new ConflictException(IdentityError.UserAlreadyExistsWithThisUserName);

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingEmail != null)
            throw new ConflictException(IdentityError.UserAlreadyExistsWithThisEmail);

        var user = new ApplicationUser
        {
            Email = request.Email,
            Name = request.Name,
            UserName = request.Email,
            EmailConfirmed = true,
            UserType = request.UserType
        };

        var registerResult = await _userManager.CreateAsync(user, request.Password);
        if (!registerResult.Succeeded)
        {
            _logger.LogError($"{IdentityError.ErrorDuringUserRegistration.Message}: {registerResult.Errors.FirstOrDefault().Description}");
            throw new ConflictException(IdentityError.ErrorDuringUserRegistration);
        }

        await _userManager.AddToRoleAsync(user, request.UserType.ToString());
        _logger.LogInformation($"User {request.Email} was succesfully registered");

        return user.Id;
    }

    public async Task<bool> ResetPassword(ResetPasswordCommand request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser == null)
            throw new NotFoundException(IdentityError.UserDoesntExist);

        var result = await _userManager.ChangePasswordAsync(existingUser, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            _logger.LogError($"{IdentityError.ErrorChangingPassword.Message}: {result.Errors.FirstOrDefault().Description}");
            throw new ConflictException(IdentityError.ErrorChangingPassword);
        }

        return result.Succeeded;
    }

    public async Task<bool> UpdatePassword(UpdatePasswordCommand request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser == null)
        {
            _logger.LogError($"{IdentityError.ErrorChangingPassword.Message}: {IdentityError.UserDoesntExist.Message}");
            throw new NotFoundException(IdentityError.UserDoesntExist);
        }
        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(existingUser);

        if (string.IsNullOrEmpty(resetToken))
        {
            _logger.LogError($"{IdentityError.ErrorChangingPassword.Message}: {IdentityError.ErrorGeneratingUpdatePassword.Message}");
            throw new ConflictException(IdentityError.ErrorGeneratingUpdatePassword);
        }

        var result = await _userManager.ResetPasswordAsync(existingUser, resetToken, request.NewPassword);
        if (!result.Succeeded)
        {
            _logger.LogError($"{IdentityError.ErrorChangingPassword.Message}: {result.Errors.FirstOrDefault().Description}");
            throw new ConflictException(IdentityError.ErrorChangingPassword);
        }

        return result.Succeeded;
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
            throw new ConflictException(IdentityError.UserDoesntExist);

        if (string.IsNullOrEmpty(user.Email))
            throw new ConflictException(IdentityError.InvalidEmailForUser);

        if (string.IsNullOrEmpty(user.UserName))
            throw new ConflictException(IdentityError.InvalidUserNameForUser);

        if (string.IsNullOrEmpty(user.SecurityStamp))
            throw new ConflictException(IdentityError.InvalidSecurtyStampNameForUser);
    }
}
