using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace Soenneker.Kiota.BearerAuthenticationProvider;

/// <summary>
/// A Kiota <see cref="IAuthenticationProvider"/> implementation that adds a Bearer token to the request's Authorization header.
/// </summary>
public sealed class BearerAuthenticationProvider : IAuthenticationProvider
{
    private readonly string _apiKey;
    private readonly AllowedHostsValidator _allowedHostsValidator;
    private readonly bool _allowInsecureHttp;

    /// <summary>Creates a provider that authenticates HTTPS requests to one host.</summary>
    public BearerAuthenticationProvider(string apiKey, string allowedHost) : this(apiKey, new[] { allowedHost })
    {
    }

    /// <summary>Creates a provider restricted to the supplied hosts.</summary>
    /// <param name="apiKey">The bearer token, without the scheme.</param>
    /// <param name="allowedHosts">Host names that may receive the token.</param>
    /// <param name="allowInsecureHttp">Whether plain HTTP requests to allowed hosts may receive the token.</param>
    public BearerAuthenticationProvider(string apiKey, IEnumerable<string> allowedHosts, bool allowInsecureHttp = false)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException("A bearer token is required.");

        string[] hosts = allowedHosts?.Where(static host => !string.IsNullOrWhiteSpace(host)).ToArray() ?? [];
        if (hosts.Length == 0)
            throw new InvalidOperationException("At least one allowed host is required.");

        _apiKey = apiKey;
        _allowedHostsValidator = new AllowedHostsValidator(hosts);
        _allowInsecureHttp = allowInsecureHttp;
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
        cancellationToken.ThrowIfCancellationRequested();
        request.Headers.Remove("Authorization");

        Uri uri = request.URI;
        bool transportAllowed = uri.Scheme == Uri.UriSchemeHttps || _allowInsecureHttp && uri.Scheme == Uri.UriSchemeHttp;

        if (!transportAllowed || !_allowedHostsValidator.IsUrlHostValid(uri))
            return Task.CompletedTask;

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        return Task.CompletedTask;
    }
}
