using Framework.Core.Constants;
using Microsoft.Extensions.Configuration;

namespace Framework.Core.Configuration;

/// <summary>
/// Loads framework configuration from a JSON configuration file.
/// </summary>
public sealed class JsonConfigurationProvider : IConfigurationProvider
{
    public TestConfiguration Load()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        TestConfiguration settings = new()
        {
            BaseUrl = new Uri(configuration[ConfigurationKeys.BaseUrl]!),
            Browser = Enum.Parse<Browser.BrowserType>(configuration[ConfigurationKeys.Browser]!, true),
            Headless = bool.Parse(configuration[ConfigurationKeys.Headless]!),
            DefaultTimeout = TimeSpan.FromSeconds(
                int.Parse(configuration[ConfigurationKeys.DefaultTimeout]!)),
            SlowMo = int.Parse(configuration[ConfigurationKeys.SlowMo]!)
        };

        return settings;
    }
}