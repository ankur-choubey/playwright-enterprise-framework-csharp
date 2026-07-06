namespace Framework.Core.Browser;

/// <summary>
/// Manages the browser lifecycle.
/// </summary>
public interface IBrowserManager
{
    Task<BrowserSession> StartAsync(
        BrowserLaunchOptions options,
        CancellationToken cancellationToken = default);

    Task StopAsync(
        BrowserSession session,
        CancellationToken cancellationToken = default);
}