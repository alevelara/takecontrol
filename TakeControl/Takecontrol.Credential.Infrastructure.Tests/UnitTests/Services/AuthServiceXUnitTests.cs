﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Takecontrol.Credential.Application.Features.Accounts.Commands.ResetPassword;
using Takecontrol.Credential.Application.Features.Accounts.Commands.UpdatePassword;
using Takecontrol.Credential.Application.Features.Accounts.Queries.Login;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Options;
using Takecontrol.Credential.Infrastructure.Models;
using Takecontrol.Credential.Infrastructure.Services;
using Takecontrol.Credential.Infrastructure.Tests.UnitTests.TestsData;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Tests.Constants;
using Xunit;

namespace Takecontrol.Credential.Infrastructure.Tests.UnitTests.Services
{
    [Trait("Category", Category.UnitTest)]
    public class AuthServiceXUnitTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManager;
        private readonly Mock<ILogger<AuthService>> _logger;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthServiceXUnitTests()
        {
            _userManager = new(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _signInManager = new(_userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), Mock.Of<IOptions<IdentityOptions>>(), Mock.Of<ILogger<SignInManager<ApplicationUser>>>(), Mock.Of<IAuthenticationSchemeProvider>(), Mock.Of<IUserConfirmation<ApplicationUser>>());
            _jwtSettings = Options.Create<JwtSettings>(new JwtSettings()
            {
                Audience = "TakeControlUsersTest",
                Issuer = "TakeControlAlevelaraTest",
                Key = Guid.NewGuid().ToString(),
                DurationInMinutes = 60
            });
            _logger = new();
        }

        [Fact]
        public async Task Login_Should_ReturnValidAuthResponse_WhenUserExistsInDb()
        {
            //Arrange
            var request = new LoginQuery("test@test.com", "password");
            var appUser = IdentityTestData.CreateApplicationUserForTest();
            _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(
                appUser
                );
            _userManager.Setup(c => c.GetClaimsAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<Claim>());
            _userManager.Setup(c => c.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>());

            _signInManager.Setup(c => c.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Act
            var response = await authService.Login(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Email, appUser.Email);
            Assert.Equal(response.Id, appUser.Id);
            Assert.Equal(response.UserName, appUser.UserName);
            Assert.Equal(response.UserType, appUser.UserType);
        }

        [Fact]
        public async Task Login_Should_ReturnException_WhenUserDoesntExist()
        {
            //Arrange
            var request = new LoginQuery("test@test.com", "password");
            _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()));
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Login(request));
        }

        [Fact]
        public async Task Login_Should_ReturnException_WhenEmailUserIsNull()
        {
            //Arrange
            var request = new LoginQuery("test@test.com", "password");
            var appUser = IdentityTestData.CreateApplicationUserForTest();
            appUser.Email = null;

            _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(appUser);
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Login(request));
        }

        [Fact]
        public async Task Login_Should_ReturnException_WhenUserNameIsNull()
        {
            //Arrange
            var request = new LoginQuery("test@test.com", "password");
            var appUser = IdentityTestData.CreateApplicationUserForTest();
            appUser.UserName = null;

            _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(appUser);
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Login(request));
        }

        [Fact]
        public async Task Login_Should_ReturnException_WhenSecurityStampIsNull()
        {
            //Arrange
            var request = new LoginQuery("test@test.com", "password");
            var appUser = IdentityTestData.CreateApplicationUserForTest();
            appUser.SecurityStamp = null;

            _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(appUser);
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Login(request));
        }

        [Fact]
        public async Task Login_Should_ReturnException_WhenSignInFailed()
        {
            //Arrange
            var request = new LoginQuery("test@test.com", "password");
            var appUser = IdentityTestData.CreateApplicationUserForTest();
            _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(
                appUser
                );

            _signInManager.Setup(c => c.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);

            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => authService.Login(request));
        }

        [Fact]
        public async Task Register_Should_ReturnConflictException_WhenUserAlreadyExistsWithSameName()
        {
            //Arrange
            var request = new RegistrationRequest("existingName", "email", "password", UserType.Club);
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);
            var applicationUser = IdentityTestData.CreateApplicationUserForTest();

            //Act
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Register(request));
        }

        [Fact]
        public async Task Register_Should_ReturnConflictException_WhenUserAlreadyExistsWithSameEmail()
        {
            //Arrange
            var request = new RegistrationRequest("name", "existingEmail", "password", UserType.Club);
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);
            var applicationUser = IdentityTestData.CreateApplicationUserForTest();

            //Act
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>()));
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Register(request));
        }

        [Fact]
        public async Task Register_Should_ReturnConflictException_WhenPasswordIsWrong()
        {
            //Arrange
            var request = new RegistrationRequest("name", "existingEmail", string.Empty, UserType.Club);
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);
            var failedIdentityResult = IdentityTestData.CreateFailedIdentityResult();

            //Act
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>()));
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>()));
            _userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(failedIdentityResult);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Register(request));
        }

        [Fact]
        public async Task Register_Should_ReturnValidUserId_WhenRegisterIsSuccesful()
        {
            //Arrange
            var request = new RegistrationRequest("name", "existingEmail", string.Empty, UserType.Club);
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);
            var applicationUser = IdentityTestData.CreateApplicationUserForTest();
            var successIdentityResult = IdentityTestData.CreateSuccededIdentityResult();

            //Act
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
            _userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(successIdentityResult);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Register(request));
        }

        [Fact]
        public async Task ResetPassword_Should_ReturnTrue_WhenCommandIsValid()
        {
            //Arrange
            var applicationUser = IdentityTestData.CreateApplicationUserForTest();
            var request = new ResetPasswordCommand(applicationUser.Email!, "Password123!", "Password124!");
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);
            var successIdentityResult = IdentityTestData.CreateSuccededIdentityResult();

            //Act
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
            _userManager.Setup(u => u.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(successIdentityResult);

            var result = await authService.ResetPassword(request);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ResetPassword_Should_ThrowNotFoundException_WhenUserDoesntExist()
        {
            //Arrange
            ApplicationUser? applicationUser = null;
            var request = new ResetPasswordCommand("user@test.com", "Password123!", "Password124!");
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Act
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => authService.ResetPassword(request));
        }

        [Fact]
        public async Task ResetPassword_Should_ThrowConflictException_WhenCurrentPasswordIsInvalid()
        {
            //Arrange
            var applicationUser = IdentityTestData.CreateApplicationUserForTest();
            var request = new ResetPasswordCommand(applicationUser.Email!, "invalidPassword", "Password124!");
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);
            var failedResult = IdentityTestData.CreateFailedIdentityResult();

            //Act
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
            _userManager.Setup(u => u.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(failedResult);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.ResetPassword(request));
        }

        [Fact]
        public void UpdatePassword_Should_ReturnTrue_WhenCommandIsValid()
        {
            //Arrange
            var applicationUser = IdentityTestData.CreateApplicationUserForTest();
            var request = new UpdatePasswordCommand(applicationUser.Email!, "Password124!");
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Act
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
            _userManager.Setup(u => u.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("resetToken");
            _userManager.Setup(u => u.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = authService.UpdatePassword(request);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task UpdatePassword_Should_ThrowNotFoundException_WhenUserDoesntExist()
        {
            //Arrange
            ApplicationUser? applicationUser = null;
            var request = new UpdatePasswordCommand("user@test.com", "Password124!");
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);

            //Act
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => authService.UpdatePassword(request));
        }

        [Fact]
        public async Task UpdatePassword_Should_ThrowConflictException_WhenChangingPasswordIsNotPossible()
        {
            //Arrange
            var applicationUser = IdentityTestData.CreateApplicationUserForTest();
            var request = new UpdatePasswordCommand(applicationUser.Email!, "invalidPassword");
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);
            var errors = new IdentityError[1];
            errors[0] = new IdentityError()
            {
                Code = "CODE",
                Description = "description"
            };

            //Act
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
            _userManager.Setup(u => u.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("resetToken");
            _userManager.Setup(u => u.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors));

            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.UpdatePassword(request));
        }
    }
}
