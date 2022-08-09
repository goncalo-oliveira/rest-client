using Faactory.RestClient;

namespace System.Net.Http;

public static class HttpClientUrlExtensions
{
    /// <summary>
    /// Sets the base url used when sending requests
    /// </summary>
    public static void SetBaseUrl( this HttpClient httpClient, string url )
    {
        httpClient.BaseAddress = new UriBuilder( url )
            .EnsurePathTrailingSlash()
            .Uri;
    }
}
