using Framework.Core.Browser;
using Framework.Tests.Base;

namespace Framework.Tests.Smoke;

[TestFixture]
public class BrowserTests : BaseTest
{

    [Test]
    public void Should_Start_Browser_Session()
    {
        Assert.Multiple(() =>
        {
            Assert.That(BrowserSession, Is.Not.Null);
            Assert.That(BrowserSession.Browser, Is.Not.Null);
            Assert.That(BrowserSession.Context, Is.Not.Null);
            Assert.That(BrowserSession.Page, Is.Not.Null);
        });
    }

    [Test]
    public async Task Should_Navigate_To_Home_Page()
    {

        await NavigationService.NavigateToHomePageAsync(
            BrowserSession,
            Configuration.BaseUrl);

        Assert.That(
            BrowserSession.Page.Url,
            Is.EqualTo(Configuration.BaseUrl.ToString()));
    }
}