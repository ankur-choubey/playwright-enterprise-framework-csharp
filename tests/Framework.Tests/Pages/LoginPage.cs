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

    private ILocator ErrorMessage =>
        Page.Locator("[data-test='error']");

    /// <summary>
    /// Returns the login error message.
    /// </summary>
    public async Task<string> GetErrorMessageAsync()
    {
         return await GetTextAsync(ErrorMessage);
    }

    /// <summary>
    /// Determines whether the login page is loaded.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> IsLoadedAsync()
    {
        return await LoginButton.IsVisibleAsync();
    }

    public async Task LoginAsync(
        string username,
        string password)
    {
        await FillAsync(
            UserNameTextBox,
            username);

        await FillAsync(
            PasswordTextBox,
            password);

        await ClickAsync(LoginButton);
    }
}