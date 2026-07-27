using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Represents the SauceDemo inventory page.
/// </summary>
public sealed class InventoryPage : BasePage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InventoryPage"/> class.
    /// </summary>
    /// <param name="page">The Playwright page.</param>
    public InventoryPage(IPage page)
        : base(page)
    {
    }

    private ILocator ShoppingCartLink =>
    Page.Locator("[data-test='shopping-cart-link']");

    private ILocator ShoppingCartBadge =>
        Page.Locator("[data-test='shopping-cart-badge']");

    private ILocator SortDropdown =>
        Page.Locator("[data-test='product-sort-container']");

    /// <summary>
    /// Determines whether the inventory page has loaded.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the inventory page is displayed; otherwise,
    /// <c>false</c>.
    /// </returns>
    public async Task<bool> IsLoadedAsync()
    {
        return await PageTitle.IsVisibleAsync();
    }

    /// <summary>
    /// Opens the shopping cart page.
    /// </summary>
    /// <returns></returns>
    public async Task<CartPage> OpenShoppingCartAsync()
    {
        await ClickAsync(ShoppingCartLink);

        return new CartPage(Page);
    }

    /// <summary>
    /// Determines whether the shopping cart badge is visible.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> IsCartBadgeVisibleAsync()
    {
        return await ShoppingCartBadge.IsVisibleAsync();
    }

    /// <summary>
    /// Gets the number of items in the shopping cart.
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetCartItemCountAsync()
    {
        string badgeText =
            await GetTextAsync(ShoppingCartBadge);

        return int.Parse(badgeText);
    }

    /// <summary>
    /// Gets the product container for the specified product name.
    /// </summary>
    /// <param name="productName"></param>
    /// <returns></returns>
    private ILocator GetProductContainer(string productName) =>
        Page.Locator(".inventory_item")
            .Filter(new() { HasText = productName });

    /// <summary>
    /// Adds the specified product to the shopping cart.
    /// </summary>
    /// <param name="productName"></param>
    /// <returns></returns>
    public async Task AddProductToCartAsync(string productName)
    {
        ILocator product =
            GetProductContainer(productName);

        await ClickAsync(
            product.Locator("button"));
    }

    /// <summary>
    /// Removes the specified product from the shopping cart.
    /// </summary>
    /// <param name="productName"></param>
    /// <returns></returns>
    public async Task RemoveProductFromCartAsync(string productName)
    {
        ILocator product =
            GetProductContainer(productName);

        await ClickAsync(
            product.Locator("button"));
    }
}