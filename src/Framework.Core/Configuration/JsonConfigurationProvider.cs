namespace Framework.Core.Configuration;

/// <summary>
/// Loads framework configuration from a JSON source.
/// </summary>
public sealed class JsonConfigurationProvider : IConfigurationProvider
{
    public TestConfiguration Load()
    {
        return new TestConfiguration();
    }
}
