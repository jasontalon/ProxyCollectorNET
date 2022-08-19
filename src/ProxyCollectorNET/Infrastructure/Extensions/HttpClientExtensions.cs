using System.Net;
using Newtonsoft.Json;

namespace ProxyCollectorNET.Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static async Task<(T? Response, Exception? Exception)> GetAsync<T>(
        this HttpClient @this,
        string url,
        CancellationToken cancellationToken = default)
        where T : class
    {
        var (response, error) = await TryCatch.HandleAsync(async () =>
        {
            var response = await @this.GetAsync(url, cancellationToken);

            var RawResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK) return null;

            var responseObject = JsonConvert.DeserializeObject<T>(RawResponse);

            return responseObject;
        });

        return (response, error);
    }
}