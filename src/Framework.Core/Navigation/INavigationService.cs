namespace Framework.Core.Navigation;

/// <summary>
/// Defines navigation operations.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigates to the configured application login page.
    /// </summary>
    Task NavigateToLoginPageAsync(
        Browser.BrowserSession session,
        Uri baseUrl,
        CancellationToken cancellationToken = default);
}