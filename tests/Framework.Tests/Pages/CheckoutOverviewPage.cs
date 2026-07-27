using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Represents the checkout overview page.
/// </summary>
public sealed class CheckoutOverviewPage : BasePage
{
    public CheckoutOverviewPage(IPage page)
        : base(page)
    {
    }

    private ILocator PageTitle =>
        Page.Locator("[data-test='title']");

    private ILocator FinishButton =>
        Page.Locator("[data-test='finish']");

    /// <summary>
    /// Determines whether the checkout overview page has loaded.
    /// </summary>
    public async Task<bool> IsLoadedAsync()
    {
        return await PageTitle.IsVisibleAsync()
            && await GetTextAsync(PageTitle) == "Checkout: Overview";
    }

    /// <summary>
    /// Completes the checkout process.
    /// </summary>
    public async Task<CheckoutCompletePage> FinishAsync()
    {
        await ClickAsync(
            FinishButton);

        return new CheckoutCompletePage(Page);
    }
}