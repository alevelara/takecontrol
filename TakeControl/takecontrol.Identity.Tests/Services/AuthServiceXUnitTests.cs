using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;
using takecontrol.Application.Exceptions;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Models.ApplicationUser.Enum;
using takecontrol.Domain.Models.ApplicationUser.Options;
using takecontrol.Identity.Models;
using takecontrol.Identity.Services;

namespace takecontrol.Identity.Tests.Services
{
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
            var appUser = CreateApplicationUserForTest();
            var user = _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(
                appUser
                );
            var claim = _userManager.Setup(c => c.GetClaimsAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<Claim>());
            var roles = _userManager.Setup(c => c.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string>());

            var signInResult = _signInManager.Setup(c => c.PasswordSignInAsync(It.IsAny<string>(),
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
            var appUser = CreateApplicationUserForTest();
            var user = _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()));
            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);            
            
            //Assert
            await Assert.ThrowsAsync<ConflictException>(() => authService.Login(request));            
        }

        [Fact]
        public async Task Login_Should_ReturnException_WhenEmailUserIsNull()
        {
            //Arrange
            var request = new LoginQuery("test@test.com", "password");
            var appUser = CreateApplicationUserForTest();
            appUser.Email = null;

            var user = _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()))
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
            var appUser = CreateApplicationUserForTest();
            appUser.UserName = null;

            var user = _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()))
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
            var appUser = CreateApplicationUserForTest();
            appUser.SecurityStamp = null;

            var user = _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>()))
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
            var appUser = CreateApplicationUserForTest();
            var user = _userManager.Setup(c => c.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(
                appUser
                );
            
            var signInResult = _signInManager.Setup(c => c.PasswordSignInAsync(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);

            var authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtSettings, _logger.Object);            

            //Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => authService.Login(request));
        }

        private ApplicationUser CreateApplicationUserForTest()
        {
            return new ApplicationUser
            {
                Email = "user@test.com",
                EmailConfirmed = true,
                Id = Guid.NewGuid(),
                UserName = "Test",
                NormalizedUserName = "TEST",
                UserType = UserType.Administrator,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
        }
    }
}
