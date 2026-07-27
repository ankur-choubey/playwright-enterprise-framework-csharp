using Framework.Tests.Base;
using Framework.Tests.Pages;
using Framework.Tests.TestData.Users;
using Framework.Tests.TestData;

namespace Framework.Tests.Smoke;

/// <summary>
/// Contains smoke tests for the inventory page.
/// </summary>
[TestFixture]
public sealed class InventoryTests : BaseTest
{
    /// <summary>
    /// Logs in using the standard user and returns the inventory page.
    /// </summary>
    private async Task<InventoryPage> LoginToInventoryAsync()
    {
        await NavigationService.NavigateToLoginPageAsync(
            BrowserSession,
            Configuration.BaseUrl);

        var loginPage = new LoginPage(BrowserSession.Page);

        return await loginPage.LoginAsync(
            LoginUsers.Standard.Username,
            LoginUsers.Standard.Password);
    }

    /// <summary>
    /// Verifies that a product can be added to the shopping cart.
    /// </summary>
    [Test]
    public async Task Should_Add_Product_To_Cart()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        // Act
        await inventoryPage.AddProductToCartAsync(
            Products.Backpack);

        bool badgeVisible =
            await inventoryPage.IsCartBadgeVisibleAsync();

        int badgeCount =
            await inventoryPage.GetCartItemCountAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(
                badgeVisible,
                Is.True,
                "The shopping cart badge should be visible after adding a product.");

            Assert.That(
                badgeCount,
                Is.EqualTo(1),
                "The shopping cart badge should display one item.");
        });
    }

    [Test]
    public async Task Should_Remove_Product_From_Cart()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        // Act
        await inventoryPage.AddProductToCartAsync(
            Products.Backpack);

        await inventoryPage.RemoveProductFromCartAsync(
            Products.Backpack);

        bool badgeVisible =
            await inventoryPage.IsCartBadgeVisibleAsync();

        // Assert
        Assert.That(
            badgeVisible,
            Is.False,
            "The shopping cart badge should not be visible after removing the only product.");
    }

    [Test]
    public async Task Should_Add_Multiple_Products_To_Cart()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        // Act
        await inventoryPage.AddProductToCartAsync(
            Products.Backpack);

        await inventoryPage.AddProductToCartAsync(
            Products.BikeLight);

        int badgeCount =
            await inventoryPage.GetCartItemCountAsync();

        // Assert
        Assert.That(
            badgeCount,
            Is.EqualTo(2),
            "The shopping cart badge should display the correct number of products.");
    }

    [Test]
    public async Task Should_Open_Shopping_Cart()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        // Act
        await inventoryPage.OpenShoppingCartAsync();

        // Assert
        Assert.That(
            BrowserSession.Page.Url,
            Does.Contain("/cart"),
            "The shopping cart page should be displayed.");
    }
}