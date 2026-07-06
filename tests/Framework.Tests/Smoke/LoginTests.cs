using Framework.Tests.Base;
using Framework.Tests.Pages;

namespace Framework.Tests.Smoke;

[TestFixture]
public sealed class LoginTests : BaseTest
{    
    [Test]
    public async Task Should_Login_With_Valid_Credentials()
    {
        await NavigationService.NavigateToHomePageAsync(
            BrowserSession,
            Configuration.BaseUrl);

        var loginPage = new LoginPage(BrowserSession.Page);

        await loginPage.LoginAsync(
            Configuration.Username,
            Configuration.Password);

        var homePage = new HomePage(BrowserSession.Page);

        Assert.That(
            await homePage.IsLoadedAsync(),
            Is.True);

        Assert.That(
            BrowserSession.Page.Url,
            Does.Contain("/inventory.html"));       
    }
}