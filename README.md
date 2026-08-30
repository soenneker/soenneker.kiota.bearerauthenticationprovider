[![](https://img.shields.io/nuget/v/soenneker.kiota.bearerauthenticationprovider.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.kiota.bearerauthenticationprovider/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.kiota.bearerauthenticationprovider/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.kiota.bearerauthenticationprovider/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.kiota.bearerauthenticationprovider/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.kiota.bearerauthenticationprovider/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.kiota.bearerauthenticationprovider.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.kiota.bearerauthenticationprovider/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.kiota.bearerauthenticationprovider/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.kiota.bearerauthenticationprovider/actions/workflows/codeql.yml)

# Soenneker.Kiota.BearerAuthenticationProvider

A host-restricted Kiota authentication provider for static bearer tokens.

## Install

```bash
dotnet add package Soenneker.Kiota.BearerAuthenticationProvider
```

## Usage

```csharp
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Kiota.BearerAuthenticationProvider;

var authentication = new BearerAuthenticationProvider(
    accessToken,
    "api.example.com");

var adapter = new HttpClientRequestAdapter(
    authentication,
    httpClient: httpClient);
```

Supply host names without a scheme or path. The multiple-host constructor accepts an `IEnumerable<string>`.

The provider replaces `Authorization` with `Bearer <token>` only when the final request URI uses HTTPS and its host is allowed. It removes an existing authorization header when the URI is not allowed, preventing a reused `RequestInformation` or Kiota raw-URL request from carrying the token to another host.

Plain HTTP is rejected by default. `allowInsecureHttp: true` exists for controlled local development only. This provider stores one static token; create a new provider when it changes, or use a Kiota access-token provider for refreshable OAuth credentials.
