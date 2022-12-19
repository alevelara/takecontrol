using Moq;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.ApplicationUser.Enum;

namespace tekecontrol.Application.Tests.Features.Account.Queries.Login;

[Trait("Category", "UnitTests")]
public class LoginQueryHandlerXUnitTests
{
    private readonly Mock<IAuthService> _authService;

    public LoginQueryHandlerXUnitTests()
    {
        _authService = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnNullResult_WhenEmailIsEmptyAsync()
    {
        //Arrange
        LoginQuery query = new("", "password");
        LoginQueryHandler handler = new(_authService.Object);
        //Act
        var result = handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.Null(result.Result);
    }

    [Fact]
    public async Task Handle_Should_ReturnNullResult_WhenPasswordIsEmptyAsync()
    {
        //Arrange
        LoginQuery query = new("user@test.com", "");
        LoginQueryHandler handler = new(_authService.Object);
        //Act
        var result = handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.Null(result.Result);
    }

    [Fact]
    public async Task Handle_Should_ReturnValidResult_WhenUserExistsAsync()
    {
        //Arrange

        LoginQuery query = new("user@test.com", "password");
        LoginQueryHandler handler = new(_authService.Object);

        var authResponse = _authService.Setup(c => c.Login(It.IsAny<LoginQuery>())).ReturnsAsync(
            new AuthResponse()
            {
                Email = "password",
                Id = Guid.NewGuid(),
                Token = "token",
                UserName = "username",
                UserType = UserType.Administrator
            });

        //Act
        var result = handler.Handle(query, default);

        //Assert
        Assert.NotNull(result.Result);
        Assert.True(result.Result is AuthResponse);
    }
}
