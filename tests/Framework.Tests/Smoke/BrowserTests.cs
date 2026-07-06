using Framework.Core.Browser;
using Framework.Core.Configuration;
using Framework.Core.Navigation;

namespace Framework.Tests.Smoke;

[TestFixture]
public class BrowserTests
{
    private static readonly IConfigurationProvider ConfigurationProvider =
            new JsonConfigurationProvider();
    private static readonly TestConfiguration Configuration =
            ConfigurationProvider.Load();
    private static readonly BrowserLaunchOptions Options = new()
    {
        Browser = Configuration.Browser,
        Headless = Configuration.Headless,
        SlowMo = Configuration.SlowMo
    };
    private static readonly IBrowserManager BrowserManager = new BrowserManager();
    private BrowserSession BrowserSession = null!;
    private readonly INavigationService NavigationService = new NavigationService();

    [Test]
    public async Task Should_Start_Browser_Session()
    {
        try
        {
            BrowserSession =
                await BrowserManager.StartAsync(Options);

            Assert.Multiple(() =>
            {
                Assert.That(BrowserSession, Is.Not.Null);
                Assert.That(BrowserSession.Browser, Is.Not.Null);
                Assert.That(BrowserSession.Context, Is.Not.Null);
                Assert.That(BrowserSession.Page, Is.Not.Null);
            });
        }

        finally
        {
            // Always close the browser session.
            if (BrowserSession != null)
            {
                await BrowserManager.StopAsync(BrowserSession);
            }
        }
    }

    [Test]
    public async Task Should_Navigate_To_Home_Page()
    {
        try
        {
            BrowserSession =
                await BrowserManager.StartAsync(Options);

            await NavigationService.NavigateToHomePageAsync(
                BrowserSession,
                Configuration.BaseUrl);

            Assert.That(
                BrowserSession.Page.Url,
                Is.EqualTo(Configuration.BaseUrl.ToString()));
        }

        finally
        {
            // Always close the browser session.
            if (BrowserSession != null)
            {
                await BrowserManager.StopAsync(BrowserSession);
            }
        }
    }
}