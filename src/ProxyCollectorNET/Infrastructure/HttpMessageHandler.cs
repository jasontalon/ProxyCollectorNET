using System.Net;
using Microsoft.Extensions.Logging;

namespace ProxyCollectorNET.Infrastructure;

public class HttpMessageHandler : DelegatingHandler
{
    private readonly ILogger<HttpMessageHandler> _logger;

    public HttpMessageHandler(ILogger<HttpMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
            _logger.LogWarning(request.RequestUri + " " + request.Method);

        return response;
    }
}