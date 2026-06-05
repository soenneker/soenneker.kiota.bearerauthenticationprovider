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
    /// Executes the authenticate request async operation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="additionalAuthenticationContext">The additional authentication context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        return Task.CompletedTask;
    }
}