using Framework.Tests.Base;

namespace Framework.Tests.Smoke;

[TestFixture]
public class ConfigurationTests : BaseTest
{
    [Test]
    public void Should_Load_Configuration()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Configuration.BaseUrl, Is.Not.Null);
            Assert.That(Configuration.Browser, Is.EqualTo(Core.Browser.BrowserType.Chromium));
        });
    }
}