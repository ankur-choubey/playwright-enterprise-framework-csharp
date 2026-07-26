namespace Framework.Tests.TestData.Users;

/// <summary>
/// Provides predefined login users for test scenarios.
/// </summary>
public static class LoginUsers
{
    public static readonly LoginUser Standard = new()
    {
        Username = "standard_user",
        Password = "secret_sauce"
    };

    public static readonly LoginUser LockedOut = new()
    {
        Username = "locked_out_user",
        Password = "secret_sauce"
    };

    public static readonly LoginUser Invalid = new()
    {
        Username = "invalid_user",
        Password = "invalid_password"
    };
}