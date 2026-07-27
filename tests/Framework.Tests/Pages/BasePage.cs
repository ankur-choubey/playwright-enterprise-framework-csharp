using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Base class for all page objects.
/// </summary>
public abstract class BasePage
{
    protected BasePage(IPage page)
    {
        Page = page;
    }

    /// <summary>
    /// Gets the Playwright page.
    /// </summary>
    protected IPage Page { get; }

    /// <summary>
    /// Gets the common page title locator.
    /// </summary>
    protected ILocator PageTitle =>
        Page.Locator("[data-test='title']");

    /// <summary>
    /// Clicks the specified locator.
    /// </summary>
    /// <param name="locator">The locator to click.</param>
    protected async Task ClickAsync(
        ILocator locator)
    {
        await locator.ClickAsync();
    }

    /// <summary>
    /// Fills the specified locator with text.
    /// </summary>
    /// <param name="locator">The locator to fill.</param>
    /// <param name="text">The text to enter.</param>
    protected async Task FillAsync(
        ILocator locator,
        string text)
    {
        await locator.FillAsync(text);
    }

    /// <summary>
    /// Returns the text of the specified locator.
    /// </summary>
    /// <param name="locator">The locator.</param>
    protected async Task<string> GetTextAsync(
        ILocator locator)
    {
        return await locator.InnerTextAsync();
    }
}