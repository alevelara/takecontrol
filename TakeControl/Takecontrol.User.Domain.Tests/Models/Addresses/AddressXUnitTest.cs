using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Domain.Models.Addresses;
using Xunit;

namespace Takecontrol.User.Domain.UnitTests.Models.Addresses;

[Trait("Category", Category.UnitTest)]
public class AddressXUnitTest
{
    [Fact]
    public void Create_Should_ReturnNewAddress_WhenAllFieldsArePopulated()
    {
        var city = "city";
        var province = "province";
        var mainAddress = "mainAddress";

        Address address = Address.Create(city, province, mainAddress);

        Assert.NotNull(address);
        Assert.Equal(address.City, city);
        Assert.Equal(address.Province, province);
        Assert.Equal(address.MainAddress, mainAddress);
    }

    [Fact]
    public void Create_Should_ReturnNewAddress_WhenMainAddressIsEmpty()
    {
        var city = "city";
        var province = "province";
        var mainAddress = string.Empty;

        Address address = Address.Create(city, province, mainAddress);

        Assert.NotNull(address);
        Assert.Equal(address.City, city);
        Assert.Equal(address.Province, province);
        Assert.Empty(address.MainAddress);
    }

    [Fact]
    public void Create_Should_ReturnNewAddress_WhenCityIsEmpty()
    {
        var city = string.Empty;
        var province = "province";
        var mainAddress = "mainAddress";

        Address address = Address.Create(city, province, mainAddress);

        Assert.NotNull(address);
        Assert.Empty(address.City);
        Assert.Equal(address.Province, province);
        Assert.Equal(address.MainAddress, mainAddress);
    }

    [Fact]
    public void Create_Should_ReturnNewAddress_WhenProvinceIsEmpty()
    {
        var city = "city";
        var province = string.Empty;
        var mainAddress = "mainAddress";

        Address address = Address.Create(city, province, mainAddress);

        Assert.NotNull(address);
        Assert.Equal(address.City, city);
        Assert.Empty(address.Province);
        Assert.Equal(address.MainAddress, mainAddress);
    }
}
