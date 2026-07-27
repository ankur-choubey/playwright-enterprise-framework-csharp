using Framework.Core.Browser;
using Framework.Core.Configuration;
using Framework.Core.Navigation;
using NUnit.Framework.Interfaces;
using Framework.Tests.Services;
using Framework.Common.Logging;
using Framework.Tests.Pages;
using Framework.Tests.TestData.Users;

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

    protected static readonly ILogger Logger = new ConsoleLogger();
    protected readonly ScreenshotService ScreenshotService;
    protected readonly TraceService TraceService;
    
    protected BaseTest()
    {
        ScreenshotService = new ScreenshotService(Logger);
        TraceService = new TraceService(Logger);
    }
    
    [SetUp]
    public async Task SetUp()
    {

        Logger.Log(
            LogLevel.Information,
            $"Starting test: {TestContext.CurrentContext.Test.Name}");

        BrowserSession =
            await BrowserManager.StartAsync(BrowserOptions);

        Logger.Log(
            LogLevel.Information,
            $"Browser started: {Configuration.Browser}");

        await TraceService.StartAsync(BrowserSession.Context);
    }

    /// <summary>
    /// Logs in using the standard user and returns the inventory page.
    /// </summary>
    protected async Task<InventoryPage> LoginToInventoryAsync()
    {
        await NavigationService.NavigateToLoginPageAsync(
            BrowserSession,
            Configuration.BaseUrl);

        var loginPage = new LoginPage(BrowserSession.Page);

        return await loginPage.LoginAsync(
            LoginUsers.Standard.Username,
            LoginUsers.Standard.Password);
    }

    [TearDown]
    protected async Task TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    Logger.Log(
                        LogLevel.Error,
                        "Test failed. Capturing artifacts.");

                    await ScreenshotService.CaptureAsync(
                        BrowserSession.Page,
                        TestContext.CurrentContext.Test.Name);

                    await TraceService.StopAsync(
                        BrowserSession.Context,
                        TestContext.CurrentContext.Test.Name);
                }
            else
            {
                await TraceService.StopAsync(BrowserSession.Context);;
            }

            Logger.Log(
                LogLevel.Information,
                $"Finished test: {TestContext.CurrentContext.Test.Name}");
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