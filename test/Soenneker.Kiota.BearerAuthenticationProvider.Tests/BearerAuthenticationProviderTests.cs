using Soenneker.Tests.HostedUnit;

namespace Soenneker.Kiota.BearerAuthenticationProvider.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class BearerAuthenticationProviderTests : HostedUnitTest
{
    public BearerAuthenticationProviderTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
