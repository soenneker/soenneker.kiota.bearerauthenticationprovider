using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace Soenneker.Kiota.BearerAuthenticationProvider;

/// <summary>
/// A Kiota <see cref="IAuthenticationProvider"/> implementation that adds a Bearer token to the request's Authorization header.
/// </summary>
public sealed class BearerAuthenticationProvider : IAuthenticationProvider
{
    private readonly string _apiKey;

    public BearerAuthenticationProvider(string apiKey)
    {
        _apiKey = apiKey;
    }

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        return Task.CompletedTask;
    }
}