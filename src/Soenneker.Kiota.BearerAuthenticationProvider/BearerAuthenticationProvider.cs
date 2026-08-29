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

    /// <summary>
    /// Authenticates request Async for the bearer authentication provider.
    /// </summary>
    /// <param name="request">request that defines the request to send.</param>
    /// <param name="additionalAuthenticationContext">additional Authentication Context to process.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the authenticate request async operation is complete.</returns>
    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        return Task.CompletedTask;
    }
}
