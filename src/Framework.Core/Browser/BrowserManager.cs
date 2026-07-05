using Microsoft.Playwright;

namespace Framework.Core.Browser;

/// <summary>
/// Default implementation of browser lifecycle management.
/// </summary>
public sealed class BrowserManager : IBrowserManager
{
    public async Task<BrowserSession> StartAsync(
        BrowserLaunchOptions options,
        CancellationToken cancellationToken = default)
    {
        IPlaywright playwright = await Playwright.CreateAsync();

        IBrowser browser = options.Browser switch
        {
            BrowserType.Chromium => await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = options.Headless,
                    SlowMo = options.SlowMo
                }),

            BrowserType.Firefox => await playwright.Firefox.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = options.Headless,
                    SlowMo = options.SlowMo
                }),

            BrowserType.Webkit => await playwright.Webkit.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = options.Headless,
                    SlowMo = options.SlowMo
                }),

            _ => throw new ArgumentOutOfRangeException(nameof(options.Browser))
        };

        IBrowserContext context = await browser.NewContextAsync();

        IPage page = await context.NewPageAsync();

        return new BrowserSession
        {
            Playwright = playwright,
            Browser = browser,
            Context = context,
            Page = page
        };
    }

    public async Task StopAsync(
        BrowserSession session,
        CancellationToken cancellationToken = default)
    {
        await session.Page.CloseAsync();

        await session.Context.CloseAsync();

        await session.Browser.CloseAsync();

        session.Playwright.Dispose();
    }
}