using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Represents the application home page.
/// </summary>
public sealed class HomePage : BasePage
{
    public HomePage(IPage page)
        : base(page)
    {
    }

    private ILocator InventoryContainer =>
       Page.Locator("#inventory_container").First;

    /// <summary>
    /// Gets the page title.
    /// </summary>
    public async Task<string> GetTitleAsync()
    {
        return await Page.TitleAsync();
    }

    /// <summary>
    /// Determines whether the home page is loaded. 
    /// </summary>
    public async Task<bool> IsLoadedAsync()
    {
        return await InventoryContainer.IsVisibleAsync();
    }
}