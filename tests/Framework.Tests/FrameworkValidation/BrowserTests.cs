using Framework.Core.Browser;
using Framework.Tests.Base;
using Framework.Tests.Pages;

namespace Framework.Tests.FrameworkValidation
{
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
        public async Task Should_Navigate_To_Login_Page()
        {
            await NavigationService.NavigateToLoginPageAsync(
                BrowserSession,
                Configuration.BaseUrl);

            var loginPage = new LoginPage(BrowserSession.Page);

            Assert.That(
                await loginPage.IsLoadedAsync(),
                Is.True);
        }
    }
}