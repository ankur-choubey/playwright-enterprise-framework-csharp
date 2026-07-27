using Framework.Tests.Base;
using Framework.Tests.Pages;
using Framework.Tests.TestData;
using Framework.Tests.TestData.Users;

namespace Framework.Tests.Smoke;

/// <summary>
/// Contains smoke tests for the checkout workflow.
/// </summary>
[TestFixture]
public sealed class CheckoutTests : BaseTest
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
    /// Verifies that a customer can successfully complete the checkout process.
    /// </summary>
    [Test]
    public async Task Should_Complete_Checkout()
    {
        // Arrange
        var inventoryPage =
            await LoginToInventoryAsync();

        // Act
        await inventoryPage.AddProductToCartAsync(
            Products.Backpack);

        var cartPage =
            await inventoryPage.OpenShoppingCartAsync();

        var checkoutInformationPage =
            await cartPage.CheckoutAsync();

        var checkoutOverviewPage =
            await checkoutInformationPage.EnterCustomerInformationAsync(
                Customers.Standard);

        var checkoutCompletePage =
            await checkoutOverviewPage.FinishAsync();

        // Assert
        Assert.That(
            await checkoutCompletePage.IsLoadedAsync(),
            Is.True,
            "The checkout process should complete successfully.");
    }
}