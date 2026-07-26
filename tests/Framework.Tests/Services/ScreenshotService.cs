using Framework.Common.Logging;
using Microsoft.Playwright;

namespace Framework.Tests.Services;

/// <summary>
/// Captures screenshots for failed test executions.
/// </summary>
public sealed class ScreenshotService
{

    private readonly ILogger _logger;

    public ScreenshotService(ILogger logger)
    {
        _logger = logger;
    }

    private static string SanitizeFileName(string fileName)
    {
        foreach (char invalidChar in Path.GetInvalidFileNameChars())
        {
            fileName = fileName.Replace(invalidChar, '_');
        }

        return fileName;
    }

    /// <summary>
    /// Captures a screenshot of the given page and saves it to a file.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="testName"></param>
    /// <returns></returns>
    public async Task CaptureAsync(
        IPage page,
        string testName)
    {

        string screenshotDirectory = Path.Combine(
        AppContext.BaseDirectory,
        "TestResults",
        "Screenshots");   

        Directory.CreateDirectory(screenshotDirectory);

        string safeTestName = SanitizeFileName(testName);

        string filePath = Path.Combine(
            screenshotDirectory,
            $"{safeTestName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

        _logger.Log(
            LogLevel.Information,
            $"Saving screenshot to '{filePath}'.");

        await page.ScreenshotAsync(new()
        {
            Path = filePath,
            FullPage = true
        });

        _logger.Log(
            LogLevel.Information,
            $"Screenshot saved to '{filePath}'.");
    }
}