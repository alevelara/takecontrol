using Moq;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Application.Features.Accounts.Queries.Login;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;
using Xunit;

namespace Takecontrol.Credential.Application.Tests.Features.Accounts.Queries.Login;

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

        _authService.Setup(c => c.Login(It.IsAny<LoginQuery>())).ReturnsAsync(
            new AuthResponse(Guid.NewGuid(), "userName", "password", "tokem", UserType.Administrator));

        //Act
        var result = handler.Handle(query, default);

        //Assert
        Assert.NotNull(result.Result);
        Assert.IsType<AuthResponse>(result.Result);
    }
}
