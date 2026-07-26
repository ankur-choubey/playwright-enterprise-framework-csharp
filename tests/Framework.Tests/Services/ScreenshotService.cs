using Microsoft.Playwright;

namespace Framework.Tests.Services;

/// <summary>
/// Captures screenshots for failed test executions.
/// </summary>
public sealed class ScreenshotService
{
    public async Task CaptureAsync(
        IPage page,
        string testName)
    {

        string screenshotDirectory = Path.Combine(
        AppContext.BaseDirectory,
        "TestResults",
        "Screenshots");   

        Directory.CreateDirectory(screenshotDirectory);

        string filePath = Path.Combine(
            screenshotDirectory,
            $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

        await page.ScreenshotAsync(new()
        {
            Path = filePath,
            FullPage = true
        });
    }
}