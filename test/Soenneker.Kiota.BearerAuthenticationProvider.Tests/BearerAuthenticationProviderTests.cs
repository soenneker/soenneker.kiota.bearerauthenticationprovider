using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Kiota.BearerAuthenticationProvider.Tests;

[Collection("Collection")]
public class BearerAuthenticationProviderTests : FixturedUnitTest
{
    public BearerAuthenticationProviderTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
