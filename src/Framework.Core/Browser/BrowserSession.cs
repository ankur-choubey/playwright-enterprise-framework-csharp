using Microsoft.Playwright;

namespace Framework.Core.Browser;

/// <summary>
/// Represents an active browser automation session.
/// </summary>
public sealed class BrowserSession
{
    public required IPlaywright Playwright { get; init; }

    public required IBrowser Browser { get; init; }

    public required IBrowserContext Context { get; init; }

    public required IPage Page { get; init; }
}