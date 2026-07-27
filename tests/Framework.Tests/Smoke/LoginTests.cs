using Framework.Tests.Base;
using Framework.Tests.Pages;
using Framework.Tests.TestData;
using Framework.Tests.TestData.Users;

namespace Framework.Tests.Smoke;

[TestFixture]
public sealed class LoginTests : BaseTest
{    
    public static IEnumerable<TestCaseData> LoginFailureScenarios()
    {
        yield return new TestCaseData(
            LoginUsers.LockedOut,
            "Epic sadface: Sorry, this user has been locked out.")
            .SetName("Should_Display_Error_For_Locked_Out_User");

        yield return new TestCaseData(
            LoginUsers.Invalid,
            "Epic sadface: Username and password do not match any user in this service")
            .SetName("Should_Display_Error_For_Invalid_User");
    }

    [Test]
    public async Task Should_Login_With_Valid_Credentials()
    {
        await NavigationService.NavigateToLoginPageAsync(
            BrowserSession,
            Configuration.BaseUrl);

        var loginPage = new LoginPage(BrowserSession.Page);

        var inventoryPage =
            await loginPage.LoginAsync(
                LoginUsers.Standard.Username,
                LoginUsers.Standard.Password);

        Assert.That(
            await inventoryPage.IsLoadedAsync(),
            Is.True,
            "The inventory page should be displayed after a successful login.");
    }

    [TestCaseSource(nameof(LoginFailureScenarios))]
    public async Task Should_Display_Error_For_Invalid_Login(
        LoginUser user,
        string expectedMessage)
    {
         await NavigationService.NavigateToLoginPageAsync(
            BrowserSession,
            Configuration.BaseUrl);

        var loginPage = new LoginPage(BrowserSession.Page);

        await loginPage.LoginAsync(
            user.Username,
            user.Password);

        string actualMessage =
            await loginPage.GetErrorMessageAsync();

        Assert.That(
            actualMessage,
            Is.EqualTo(expectedMessage));
    }
}