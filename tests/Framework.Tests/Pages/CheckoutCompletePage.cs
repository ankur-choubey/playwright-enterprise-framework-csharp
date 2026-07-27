using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Represents the checkout completion page.
/// </summary>
public sealed class CheckoutCompletePage : BasePage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CheckoutCompletePage"/> class.
    /// </summary>
    /// <param name="page">The Playwright page.</param>
    public CheckoutCompletePage(IPage page)
        : base(page)
    {
    }

    private ILocator CompleteHeader =>
        Page.Locator("[data-test='complete-header']");

    /// <summary>
    /// Determines whether the checkout complete page has loaded.
    /// </summary>
    public async Task<bool> IsLoadedAsync()
    {
        return await PageTitle.IsVisibleAsync()
            && await GetTextAsync(PageTitle) == "Checkout: Complete!"
            && await CompleteHeader.IsVisibleAsync();
    }
}