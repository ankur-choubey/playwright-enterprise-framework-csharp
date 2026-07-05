using Framework.Core.Configuration;
using NUnit.Framework;

namespace Framework.Tests.Smoke;

[TestFixture]
public class ConfigurationTests
{
    [Test]
    public void Should_Load_Configuration()
    {
        IConfigurationProvider provider = new JsonConfigurationProvider();

        TestConfiguration configuration = provider.Load();

        Assert.Multiple(() =>
        {
            Assert.That(configuration.BaseUrl, Is.Not.Null);
            Assert.That(configuration.Browser, Is.EqualTo(Core.Browser.BrowserType.Chromium));
        });
    }
}