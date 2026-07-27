using Microsoft.Playwright;

namespace Framework.Tests.Pages;

/// <summary>
/// Represents the first step of the checkout process.
/// </summary>
public sealed class CheckoutInformationPage : BasePage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CheckoutInformationPage"/> class.
    /// </summary>
    public CheckoutInformationPage(IPage page)
        : base(page)
    {
    }

    private ILocator PageTitle =>
        Page.Locator("[data-test='title']");

    private ILocator FirstNameTextBox =>
        Page.Locator("[data-test='firstName']");

    private ILocator LastNameTextBox =>
        Page.Locator("[data-test='lastName']");

    private ILocator PostalCodeTextBox =>
        Page.Locator("[data-test='postalCode']");

    private ILocator ContinueButton =>
        Page.Locator("[data-test='continue']");

    /// <summary>
    /// Determines whether the checkout information page has loaded.
    /// </summary>
    public async Task<bool> IsLoadedAsync()
    {
        return await PageTitle.IsVisibleAsync()
            && await GetTextAsync(PageTitle) == "Checkout: Your Information";
    }

    /// <summary>
    /// Enters the customer's checkout information and continues to the next step.
    /// </summary>
    public async Task<CheckoutOverviewPage> EnterCustomerInformationAsync(
        Customer customer)
    {
        await FillAsync(
            FirstNameTextBox,
            customer.FirstName);

        await FillAsync(
            LastNameTextBox,
            customer.LastName);

        await FillAsync(
            PostalCodeTextBox,
            customer.PostalCode);

        await ClickAsync(
            ContinueButton);

        return new CheckoutOverviewPage(Page);
    }
}