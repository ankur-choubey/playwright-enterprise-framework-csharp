using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Represents the SauceDemo shopping cart page.
/// </summary>
public sealed class CartPage : BasePage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CartPage"/> class.
    /// </summary>
    /// <param name="page">The Playwright page.</param>
    public CartPage(IPage page)
        : base(page)
    {
    }

    private ILocator CartItems =>
        Page.Locator(".cart_item");

    private ILocator ContinueShoppingButton =>
        Page.Locator("[data-test='continue-shopping']");

    private ILocator CheckoutButton =>
        Page.Locator("[data-test='checkout']");

    /// <summary>
    /// Determines whether the cart page has loaded.
    /// </summary>
    public async Task<bool> IsLoadedAsync()
    {
        return await PageTitle.IsVisibleAsync()
            && await GetTextAsync(PageTitle) == "Your Cart";
    }

    /// <summary>
    /// Gets the product container for the specified product name.
    /// </summary>
    /// <param name="productName"></param>
    /// <returns></returns>
    private ILocator GetProductContainer(string productName) =>
        CartItems.Filter(new() { HasText = productName });

    /// <summary>
    /// Determines whether the specified product exists in the shopping cart.
    /// </summary>
    public async Task<bool> ContainsProductAsync(string productName)
    {
        return await GetProductContainer(productName)
            .IsVisibleAsync();
    }

    /// <summary>
    /// Removes a product from the shopping cart.
    /// </summary>
    public async Task RemoveProductAsync(string productName)
    {
        var product =
            GetProductContainer(productName);

        await ClickAsync(
            product.Locator("button"));
    }

    /// <summary>
    /// Returns to the inventory page.
    /// </summary>
    public async Task<InventoryPage> ContinueShoppingAsync()
    {
        await ClickAsync(
            ContinueShoppingButton);

        return new InventoryPage(Page);
    }

    /// <summary>
    /// Determines whether the shopping cart is empty.
    /// </summary>
    public async Task<bool> IsEmptyAsync()
    {
        return await CartItems.CountAsync() == 0;
    }

    /// <summary>
    /// Navigates to the checkout information page.
    /// </summary>
    public async Task<CheckoutInformationPage> CheckoutAsync()
    {
        await ClickAsync(
            CheckoutButton);

        return new CheckoutInformationPage(Page);
    }
}