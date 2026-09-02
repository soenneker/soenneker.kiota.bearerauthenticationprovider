using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;
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

    [Test]
    public async Task Adds_token_only_for_allowed_https_hosts(CancellationToken cancellationToken)
    {
        var provider = new BearerAuthenticationProvider("secret", "api.example.com");
        var request = new RequestInformation { URI = new System.Uri("https://api.example.com/v1/items") };

        await provider.AuthenticateRequestAsync(request, cancellationToken: cancellationToken);

        await Assert.That(request.Headers.TryGetValue("Authorization", out IEnumerable<string>? values)).IsTrue();
        await Assert.That(values!.Single()).IsEqualTo("Bearer secret");

        request.URI = new System.Uri("https://attacker.example/v1/items");
        await provider.AuthenticateRequestAsync(request, cancellationToken: cancellationToken);

        await Assert.That(request.Headers.ContainsKey("Authorization")).IsFalse();
    }

    [Test]
    public async Task Does_not_send_token_over_plain_http_by_default(CancellationToken cancellationToken)
    {
        var provider = new BearerAuthenticationProvider("secret", "api.example.com");
        var request = new RequestInformation { URI = new System.Uri("http://api.example.com/v1/items") };

        await provider.AuthenticateRequestAsync(request, cancellationToken: cancellationToken);

        await Assert.That(request.Headers.ContainsKey("Authorization")).IsFalse();
    }
}
