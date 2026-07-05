namespace Framework.Core.Browser;

/// <summary>
/// Represents browser launch settings.
/// </summary>
public sealed class BrowserLaunchOptions
{
    public BrowserType Browser { get; init; } = BrowserType.Chromium;

    public bool Headless { get; init; } = true;

    public int SlowMo { get; init; }

    public string[] Arguments { get; init; } = [];
}