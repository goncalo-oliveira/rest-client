using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Faactory.RestClient;

/// <summary>
/// A pre-configured request
/// </summary>
internal sealed class RestRequest : IRestClient
{
    private readonly RestRequestOptions options;

    internal RestRequest( IRestClient restClient, RestRequestOptions requestOptions )
    {
        Client = restClient;
        options = requestOptions;
    }

    /// <summary>
    /// Gets the underlying RestClient instance
    /// </summary>
    public IRestClient Client { get; }

    public HttpClient HttpClient => Client.HttpClient;

    public System.Collections.Generic.IEnumerable<IRestPreprocessor> Preprocessors => Client.Preprocessors;

    public ISerializer Serializer => Client.Serializer;

    public System.Net.Http.Headers.HttpRequestHeaders Headers => options.Headers;

    public IRestClient Configure( Action<RestRequestOptions> configure )
    {
        configure( options );

        return ( this );
    }

    /// <summary>
    /// Sends a GET request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location; overwrites pre-configured location</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> GetAsync( string url, CancellationToken cancellationToken = default )
        => Client.SendASync( Configure, HttpMethod.Get, url, null, cancellationToken );

    /// <summary>
    /// Sends a POST request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location; overwrites pre-configured location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> PostAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
        => Client.SendASync( Configure, HttpMethod.Post, url, content, cancellationToken );

    /// <summary>
    /// Sends a PUT request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location; overwrites pre-configured location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> PutAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
        => Client.SendASync( Configure, HttpMethod.Put, url, content, cancellationToken );

    /// <summary>
    /// Sends a PATCH request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location; overwrites pre-configured location</param>
    /// <param name="content">The request body content</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> PatchAsync( string url, HttpContent content, CancellationToken cancellationToken = default )
        => Client.SendASync( Configure, HttpMethod.Patch, url, content, cancellationToken );

    /// <summary>
    /// Sends a DELETE request to the specified resource location
    /// </summary>
    /// <param name="url">The resource location; overwrites pre-configured location</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation</param>
    public Task<RestResponse> DeleteAsync( string url, CancellationToken cancellationToken = default )
        => Client.SendASync( Configure, HttpMethod.Delete, url, null, cancellationToken );

    private void Configure( HttpRequestMessage message )
    {
        if ( options.Headers.Any() )
        {
            message.Headers.CopyFrom( options.Headers );
        }

        // include additional query parameters (for specific/override location)
        if ( options.QueryParameters.HasKeys() )
        {
            var resourceUrl = ResourceUrl.FromUri( message.RequestUri! );

            // append/overwrite with configured query parameters
            foreach ( var key in options.QueryParameters.AllKeys )
            {
                resourceUrl.QueryParameters.Set( key, options.QueryParameters[key] );
            }

            message.RequestUri = resourceUrl;
        }

        // set HTTP version and policy
        if ( options.Version is not null )
        {
            message.Version = options.Version;
        }

        if ( options.VersionPolicy is not null )
        {
            message.VersionPolicy = options.VersionPolicy.Value;
        }
    }
}
