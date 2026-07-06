using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Represents the SauceDemo login page.
/// </summary>
public sealed class LoginPage : BasePage
{
    public LoginPage(IPage page)
        : base(page)
    {
    }

    private ILocator UserNameTextBox =>
        Page.Locator("#user-name");

    private ILocator PasswordTextBox =>
        Page.Locator("#password");

    private ILocator LoginButton =>
        Page.Locator("#login-button");

    public async Task LoginAsync(
        string username,
        string password)
    {
        await UserNameTextBox.FillAsync(username);

        await PasswordTextBox.FillAsync(password);

        await LoginButton.ClickAsync();
    }
}