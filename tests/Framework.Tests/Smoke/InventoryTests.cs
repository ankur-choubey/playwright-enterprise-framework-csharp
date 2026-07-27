using Allure.NUnit;
using Framework.Tests.Base;
using Framework.Tests.Pages;
using Framework.Tests.TestData;

namespace Framework.Tests.Smoke;

/// <summary>
/// Contains smoke tests for the inventory page.
/// </summary>
[TestFixture]
[Category("Smoke")]
[AllureNUnit]
public sealed class InventoryTests : BaseTest
{
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

    /// <summary>
    /// Verifies that a product can be removed from the shopping cart.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Verifies that multiple products can be added to the shopping cart and that the badge count is correct.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Verifies that the shopping cart can be opened from the inventory page.
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Should_Open_Shopping_Cart()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        CartPage cartPage =
            await inventoryPage.OpenShoppingCartAsync();

        Assert.That(
            await cartPage.IsLoadedAsync(),
            Is.True,
            "The shopping cart page should be displayed.");
    }

    /// <summary>
    /// Verifies that an added product is displayed in the shopping cart.
    /// </summary>
    [Test]
    public async Task Should_Display_Added_Product_In_Cart()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        // Act
        await inventoryPage.AddProductToCartAsync(
            Products.Backpack);

        var cartPage =
            await inventoryPage.OpenShoppingCartAsync();

        bool containsProduct =
            await cartPage.ContainsProductAsync(
                Products.Backpack);

        // Assert
        Assert.That(
            containsProduct,
            Is.True,
            "The added product should be displayed in the shopping cart.");
    }

    /// <summary>
    /// Verifies that a product can be removed from the shopping cart.  
    /// </summary>
    [Test]
    public async Task Should_Remove_Product_From_Cart_Page()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        await inventoryPage.AddProductToCartAsync(
            Products.Backpack);

        var cartPage =
            await inventoryPage.OpenShoppingCartAsync();

        // Act
        await cartPage.RemoveProductAsync(
            Products.Backpack);

        bool containsProduct =
            await cartPage.ContainsProductAsync(
                Products.Backpack);

        bool isEmpty =
            await cartPage.IsEmptyAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(
                containsProduct,
                Is.False,
                "The removed product should no longer be displayed.");

            Assert.That(
                isEmpty,
                Is.True,
                "The shopping cart should be empty.");
        });
    }

    /// <summary>
    /// Verifies that the user can return to the inventory page.
    /// </summary>
    [Test]
    public async Task Should_Continue_Shopping()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        var cartPage =
            await inventoryPage.OpenShoppingCartAsync();

        // Act
        inventoryPage =
            await cartPage.ContinueShoppingAsync();

        // Assert
        Assert.That(
            await inventoryPage.IsLoadedAsync(),
            Is.True,
            "The inventory page should be displayed after continuing shopping.");
    }
}