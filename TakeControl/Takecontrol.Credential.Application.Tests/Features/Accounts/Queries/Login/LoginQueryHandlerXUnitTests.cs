using Moq;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Application.Features.Accounts.Queries.Login;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;
using Takecontrol.Shared.Tests.Constants;
using Xunit;

namespace Takecontrol.Credential.Application.Tests.Features.Accounts.Queries.Login;

[Trait("Category", Category.UnitTest)]
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
        LoginQuery query = new(string.Empty, "password");
        LoginQueryHandler handler = new(_authService.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_Should_ReturnNullResult_WhenPasswordIsEmptyAsync()
    {
        //Arrange
        LoginQuery query = new("user@test.com", string.Empty);
        LoginQueryHandler handler = new(_authService.Object);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.Null(result);
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
        var result = await handler.Handle(query, default);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<AuthResponse>(result);
    }
}
