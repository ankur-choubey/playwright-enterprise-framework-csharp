using Framework.Core.Browser;
using Framework.Core.Configuration;
using Framework.Core.Navigation;
using NUnit.Framework.Interfaces;
using Framework.Tests.Services;

namespace Framework.Tests.Base;

/// <summary>
/// Base class for all UI tests.
/// </summary>
public abstract class BaseTest
{
    protected static readonly IConfigurationProvider ConfigurationProvider =
        new JsonConfigurationProvider();

    protected static readonly TestConfiguration Configuration =
        ConfigurationProvider.Load();

    protected static readonly BrowserLaunchOptions BrowserOptions = new()
    {
        Browser = Configuration.Browser,
        Headless = Configuration.Headless,
        SlowMo = Configuration.SlowMo
    };

    protected static readonly IBrowserManager BrowserManager =
        new BrowserManager();

    protected readonly INavigationService NavigationService =
        new NavigationService();

    protected BrowserSession BrowserSession = null!;

    protected readonly ScreenshotService ScreenshotService =
    new();

    protected readonly TraceService TraceService = new();

    [SetUp]
    public async Task SetUp()
    {
        BrowserSession =
            await BrowserManager.StartAsync(BrowserOptions);

        await TraceService.StartAsync(BrowserSession.Context);
    }

    [TearDown]
    protected async Task TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    await ScreenshotService.CaptureAsync(
                        BrowserSession.Page,
                        TestContext.CurrentContext.Test.Name);

                    await TraceService.StopAsync(
                        BrowserSession.Context,
                        TestContext.CurrentContext.Test.Name);
                }
            else
            {
                await BrowserSession.Context.Tracing.StopAsync();
            }
        }
        finally
        {
            if (BrowserSession != null)
            {
                await BrowserManager.StopAsync(BrowserSession);
            }
        }
    }
}