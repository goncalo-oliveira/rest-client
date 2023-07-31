using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

internal static class RestClientSendExtensions
{
    public static async Task<RestResponse> SendASync( this IRestClient client, Action<HttpRequestMessage> configure, HttpMethod method, string url, HttpContent content = null, CancellationToken cancellationToken = default )
    {
        if ( url == null )
        {
            throw new ArgumentNullException( nameof( url ), "Request URL cannot be null" );
        }

        var message = new HttpRequestMessage( method, url )
        {
            Content = content
        };

        // TODO: execute preprocessors
        if ( client.Preprocessors.Any() )
        {
            foreach ( var preprocessor in client.Preprocessors )
            {
                await preprocessor.ExecuteAsync( message, cancellationToken );
            }
        }

        configure?.Invoke( message );

        var chrono = System.Diagnostics.Stopwatch.StartNew();
        var httpResponse = await client.HttpClient.SendAsync( message, HttpCompletionOption.ResponseHeadersRead, cancellationToken );
        
        chrono.Stop();

        if ( cancellationToken.IsCancellationRequested )
        {
            httpResponse.Dispose();

            return ( RestResponse.Empty );
        }

        var restResponse = new RestResponse
        {
            Serializer = client.Serializer,
            StatusCode = (int)httpResponse.StatusCode,
            Headers = httpResponse.Headers,
            ContentType = httpResponse.Content?.Headers.ContentType?.MediaType,
            Duration = chrono.Elapsed,
            Version = httpResponse.Version
        };

        try
        {
            restResponse.Content = await httpResponse.Content.ReadAsByteArrayAsync( cancellationToken );
        }
        finally
        {
            httpResponse.Dispose();
        }

        return ( restResponse );
    }
}
