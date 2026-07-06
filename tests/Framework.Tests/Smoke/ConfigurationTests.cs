using Framework.Core.Configuration;

namespace Framework.Tests.Smoke;

[TestFixture]
public class ConfigurationTests
{
    private static readonly IConfigurationProvider ConfigurationProvider =
        new JsonConfigurationProvider();
    private TestConfiguration Configuration = null!;

    [Test]
    public void Should_Load_Configuration()
    {
        try
        {
            Configuration = ConfigurationProvider.Load();

            Assert.Multiple(() =>
            {
                Assert.That(Configuration.BaseUrl, Is.Not.Null);
                Assert.That(Configuration.Browser, Is.EqualTo(Core.Browser.BrowserType.Chromium));
            });
        }
        finally
        {
            // Cleanup if needed
        }
    }
}