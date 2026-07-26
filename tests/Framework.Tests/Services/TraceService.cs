using Microsoft.Playwright;

namespace Framework.Tests.Services;

/// <summary>
/// Manages Playwright tracing for test execution.
/// </summary>
public sealed class TraceService
{
    public async Task StartAsync(IBrowserContext context)
    {
        await context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    public async Task StopAsync(
        IBrowserContext context,
        string testName)
    {
        string traceDirectory = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "TestResults",
            "Traces");

        Directory.CreateDirectory(traceDirectory);

        string tracePath = Path.Combine(
            traceDirectory,
            $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.zip");

        await context.Tracing.StopAsync(new()
        {
            Path = tracePath
        });
    }
}