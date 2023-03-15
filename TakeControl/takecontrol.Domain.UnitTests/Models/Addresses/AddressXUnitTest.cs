using Takecontrol.Domain.Models.Addresses;

namespace Takecontrol.Domain.UnitTests.Models.Addresses;

[Trait("Category", "UnitTests")]
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
        var mainAddress = "";

        Address address = Address.Create(city, province, mainAddress);

        Assert.NotNull(address);
        Assert.Equal(address.City, city);
        Assert.Equal(address.Province, province);
        Assert.Empty(address.MainAddress);
    }

    [Fact]
    public void Create_Should_ReturnNewAddress_WhenCityIsEmpty()
    {
        var city = "";
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
        var province = "";
        var mainAddress = "mainAddress";

        Address address = Address.Create(city, province, mainAddress);

        Assert.NotNull(address);
        Assert.Equal(address.City, city);
        Assert.Empty(address.Province);
        Assert.Equal(address.MainAddress, mainAddress);
    }
}
