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
}