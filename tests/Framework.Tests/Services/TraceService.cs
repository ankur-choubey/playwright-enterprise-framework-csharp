using Framework.Common.Logging;
using Microsoft.Playwright;

namespace Framework.Tests.Services;

/// <summary>
/// Manages Playwright tracing for test execution.
/// </summary>
public sealed class TraceService
{
    private readonly ILogger _logger;

    public TraceService(ILogger logger)
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
    /// Starts Playwright tracing for the given browser context.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task StartAsync(IBrowserContext context)
    {
         _logger.Log(
            LogLevel.Information,
            "Starting Playwright tracing.");

        await context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    /// <summary>
    /// Stops Playwright tracing and saves the trace data to a file.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="testName"></param>
    /// <returns></returns>
    public async Task StopAsync(
        IBrowserContext context,
        string testName)
    {
        string traceDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "TestResults",
            "Traces");

        Directory.CreateDirectory(traceDirectory);

        string safeTestName = SanitizeFileName(testName);
        
        string tracePath = Path.Combine(
            traceDirectory,
            $"{safeTestName}_{DateTime.Now:yyyyMMdd_HHmmss}.zip");

       _logger.Log(
            LogLevel.Information,
            $"Saving trace to '{tracePath}'.");

        await context.Tracing.StopAsync(new()
        {
            Path = tracePath
        });

        _logger.Log(
            LogLevel.Information,
            $"Trace saved to '{tracePath}'.");
    }

    /// <summary>
    /// Stops Playwright tracing without saving the trace data.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task StopAsync(IBrowserContext context)
    {
       _logger.Log(
            LogLevel.Information,
            "Stopping Playwright tracing without saving.");

        await context.Tracing.StopAsync();
    }
}