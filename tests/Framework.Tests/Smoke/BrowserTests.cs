using Framework.Core.Browser;
using Framework.Core.Configuration;
using NUnit.Framework;

namespace Framework.Tests.Smoke;

[TestFixture]
public class BrowserTests
{
    [Test]
    public async Task Should_Launch_Chromium()
    {
        IConfigurationProvider configurationProvider =
            new JsonConfigurationProvider();

        TestConfiguration configuration =
            configurationProvider.Load();

        BrowserLaunchOptions options = new()
        {
            Browser = configuration.Browser,
            Headless = configuration.Headless,
            SlowMo = configuration.SlowMo
        };

        IBrowserManager browserManager = new BrowserManager();

        BrowserSession session =
            await browserManager.StartAsync(options);

        Assert.That(session, Is.Not.Null);
        Assert.That(session.Browser, Is.Not.Null);
        Assert.That(session.Context, Is.Not.Null);
        Assert.That(session.Page, Is.Not.Null);

        await browserManager.StopAsync(session);
    }
}