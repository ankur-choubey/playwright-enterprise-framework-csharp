using Framework.Core.Browser;
using Framework.Core.Configuration;
using Framework.Core.Navigation;

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

    [SetUp]
    public async Task SetUp()
    {
        BrowserSession =
            await BrowserManager.StartAsync(BrowserOptions);
    }

    [TearDown]
    public async Task TearDown()
    {
        if (BrowserSession != null)
        {
            await BrowserManager.StopAsync(BrowserSession);
        }
    }
}