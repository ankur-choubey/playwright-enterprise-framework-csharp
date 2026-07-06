using Framework.Core.Browser;

namespace Framework.Core.Configuration;

/// <summary>
/// Represents the configuration used during test execution.
/// </summary>
public sealed class TestConfiguration
{
    /// <summary>
    /// Gets the application base URL.
    /// </summary>
    public Uri BaseUrl { get; init; } = new("https://www.saucedemo.com");

    /// <summary>
    /// Gets the browser used for test execution.
    /// </summary>
    public BrowserType Browser { get; init; } = BrowserType.Chromium;

    /// <summary>
    /// Indicates whether tests should run in headless mode.
    /// </summary>
    public bool Headless { get; init; } = true;

    /// <summary>
    /// Gets the default timeout for browser operations.
    /// </summary>
    public TimeSpan DefaultTimeout { get; init; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Gets the delay between browser actions.
    /// </summary>
    public int SlowMo { get; init; } = 0;

}
