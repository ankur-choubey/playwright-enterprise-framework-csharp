using Framework.Core.Browser;

namespace Framework.Core.Navigation;

/// <summary>
/// Provides browser navigation operations.
/// </summary>
public sealed class NavigationService : INavigationService
{
    public async Task NavigateToHomePageAsync(
        BrowserSession session,
        Uri baseUrl,
        CancellationToken cancellationToken = default)
    {
        await session.Page.GotoAsync(
        baseUrl.ToString(),
        new()
        {
            WaitUntil = Microsoft.Playwright.WaitUntilState.NetworkIdle
        });
    }
}