namespace Framework.Core.Navigation;

/// <summary>
/// Defines navigation operations.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigates to the configured application home page.
    /// </summary>
    Task NavigateToHomePageAsync(
        Browser.BrowserSession session,
        Uri baseUrl,
        CancellationToken cancellationToken = default);
}