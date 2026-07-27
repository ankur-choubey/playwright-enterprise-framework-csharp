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

    private ILocator ProductsTitle =>
        Page.Locator("[data-test='title']");

    /// <summary>
    /// Determines whether the inventory page has loaded.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the inventory page is displayed; otherwise,
    /// <c>false</c>.
    /// </returns>
    public async Task<bool> IsLoadedAsync()
    {
        return await ProductsTitle.IsVisibleAsync();
    }
}