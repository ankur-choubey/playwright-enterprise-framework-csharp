namespace Framework.Tests.TestData;

/// <summary>
/// Represents login credentials used by UI tests.
/// </summary>
public sealed class LoginUser
{
    public required string Username { get; init; }

    public required string Password { get; init; }
}